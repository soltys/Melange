using System.Collections.Generic;

namespace Soltys.Library.VisualStudioSolution
{
    public class AstProject
    {
        public AstProject(string kind, string name, string path, string guid)
        {
            Kind = kind;
            Name = name;
            Path = path;
            Guid = guid;
        }

        public string Kind
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public string Guid
        {
            get;
            set;
        }

        public AstProjectSection? ProjectSection
        {
            get;
            set;
        }

    }

    public class AstProjectSection
    {
        public AstProjectSection(string kind, string action, IEnumerable<SectionEntry> entries)
        {
            Kind = kind;
            Action = action;
            Entries = entries;
        }

        public string Kind
        {
            get;
            set;
        }

        public string Action
        {
            get;
            set;
        }

        public IEnumerable<SectionEntry> Entries
        {
            get;
            set;
        }

    }
}
