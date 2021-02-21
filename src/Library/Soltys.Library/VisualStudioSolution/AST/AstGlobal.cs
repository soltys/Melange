using System.Collections.Generic;

namespace Soltys.Library.VisualStudioSolution
{
    public class AstGlobal
    {
        public AstGlobal(IEnumerable<AstGlobalSection> sections)
        {
            Sections = sections;
        }

        public IEnumerable<AstGlobalSection> Sections
        {
            get;
            set;
        }
    }
}
