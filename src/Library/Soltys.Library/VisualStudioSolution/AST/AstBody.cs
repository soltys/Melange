using System.Collections.Generic;

namespace Soltys.Library.VisualStudioSolution
{
    public class AstBody
    {
        public AstBody(IEnumerable<AstProject> projects, AstGlobal global)
        {
            Projects = projects;
            Global = global;
        }

        public IEnumerable<AstProject> Projects
        {
            get;
            set;
        }

        public AstGlobal Global
        {
            get;
            set;
        }
    }
}
