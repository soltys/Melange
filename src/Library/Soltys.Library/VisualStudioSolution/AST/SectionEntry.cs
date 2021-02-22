using System.Collections.Generic;

namespace Soltys.Library.VisualStudioSolution
{
    public class SectionEntry
    {
        public SectionEntry(IEnumerable<SolutionToken> tokens)
        {
            Tokens = tokens;
        }

        public IEnumerable<SolutionToken> Tokens
        {
            get;
            set;
        }
    }
}
