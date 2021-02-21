using System;

namespace Soltys.Library.VisualStudioSolution
{
    public class NestedProjectsEntry : SectionEntry
    {
        public NestedProjectsEntry(string line) : base(line)
        {
            
        }

        public Guid Parent
        {
            get;
            set;
        }

        public Guid Child
        {
            get;
            set;
        }
    }
}
