using System;
using System.Collections.Generic;

namespace Soltys.Library.VisualStudioSolution
{
    public class NestedProjectsEntry : SectionEntry
    {
        public NestedProjectsEntry(IEnumerable<SolutionToken> tokens) : base(tokens)
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
