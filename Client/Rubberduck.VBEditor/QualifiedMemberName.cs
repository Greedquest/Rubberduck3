using System;

namespace Rubberduck.VBEditor
{
    public readonly struct QualifiedMemberName
    {
        public static QualifiedMemberName None { get; } = new QualifiedMemberName(QualifiedModuleName.None, string.Empty);

        public QualifiedMemberName(QualifiedModuleName qualifiedModuleName, string memberName)
        {
            QualifiedModuleName = qualifiedModuleName;
            MemberName = memberName;
        }
        
        public QualifiedModuleName QualifiedModuleName { get; }
        public string MemberName { get; }

        public override string ToString()
        {
            return $"{QualifiedModuleName}.{MemberName}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(QualifiedModuleName, MemberName);
        }

        public override bool Equals(object obj)
        {
            try
            {
                var other = (QualifiedMemberName)obj;
                return QualifiedModuleName.Equals(other.QualifiedModuleName) && MemberName == other.MemberName;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        public static bool operator ==(QualifiedMemberName a, QualifiedMemberName b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(QualifiedMemberName a, QualifiedMemberName b)
        {
            return !a.Equals(b);
        }
    }
}
