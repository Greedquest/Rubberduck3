using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Input;
using NLog;
using Rubberduck.InternalApi.Common;

namespace Rubberduck.UI.Command
{

    [ComVisible(false)]
    public abstract class CommandBase : ICommand
    {
        private static readonly List<MethodBase> ExceptionTargetSites = new List<MethodBase>();

        protected CommandBase(ILogger logger = null)
        {
            Logger = logger ?? LogManager.GetLogger(GetType().FullName);
            CanExecuteCondition = (parameter => true);
            OnExecuteCondition = (parameter => true);
        }

        protected ILogger Logger { get; }
        protected abstract void OnExecute(object parameter);

        protected Func<object, bool> CanExecuteCondition { get; private set; }
        protected Func<object, bool> OnExecuteCondition { get; private set; }

        protected void AddToCanExecuteEvaluation(Func<object, bool> furtherCanExecuteEvaluation, bool requireReevaluationAlso = false)
        {
            if (furtherCanExecuteEvaluation == null)
            {
                return;
            }

            var currentCanExecuteCondition = CanExecuteCondition;
            CanExecuteCondition = (parameter) =>
                currentCanExecuteCondition(parameter) && furtherCanExecuteEvaluation(parameter);

            if (requireReevaluationAlso)
            {
                AddToOnExecuteEvaluation(furtherCanExecuteEvaluation);
            }
        }

        protected void AddToOnExecuteEvaluation(Func<object, bool> furtherCanExecuteEvaluation)
        {
            if (furtherCanExecuteEvaluation == null)
            {
                return;
            }

            var currentOnExecute = OnExecuteCondition;
            OnExecuteCondition = (parameter) => 
                currentOnExecute(parameter) && furtherCanExecuteEvaluation(parameter);
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                var result = false;
                var elapsed = TimedAction.Run(() => result = CanExecuteCondition(parameter));
                Logger.Trace($"{GetType().Name}.CanExecute completed in {elapsed.TotalMilliseconds}ms.");
                return result;
            }
            catch (Exception exception)
            {
                Logger.Error(exception);

                if (!ExceptionTargetSites.Contains(exception.TargetSite))
                {
                    ExceptionTargetSites.Add(exception.TargetSite);
                }

                return false;
            }
        }

        public void Execute(object parameter)
        {
            try
            {
                var elapsed = TimedAction.Run(() =>
                {
                    if (!OnExecuteCondition(parameter))
                    {
                        return;
                    }

                    OnExecute(parameter);
                });
                Logger.Trace($"{GetType().Name}.Execute completed in {elapsed.TotalMilliseconds}ms.");
            }
            catch (Exception exception)
            {
                Logger.Error(exception);

                if (!ExceptionTargetSites.Contains(exception.TargetSite))
                {
                    ExceptionTargetSites.Add(exception.TargetSite);
                }
            }
        }

        public string ShortcutText { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
