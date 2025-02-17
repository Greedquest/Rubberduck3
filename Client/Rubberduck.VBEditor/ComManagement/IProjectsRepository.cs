﻿using Rubberduck.InternalApi.Model;

namespace Rubberduck.VBEditor.ComManagement
{
    public interface IProjectsRepository : IProjectsProvider
    {
        void Refresh();
        void Refresh(string projectId);
        void RemoveComponent(IQualifiedModuleName qualifiedModuleName);
    }
}
