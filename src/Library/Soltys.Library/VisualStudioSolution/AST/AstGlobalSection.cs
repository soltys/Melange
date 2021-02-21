using System.Collections.Generic;

namespace Soltys.Library.VisualStudioSolution
{
    public class AstGlobalSection
    {
        public AstGlobalSection(string kind, string action, IEnumerable<SectionEntry> entries)
        {
            Kind = kind;
            Action = action;
            Entries = entries;
        }

        /// <summary>
        /// Example: SolutionConfigurationPlatforms or ProjectConfigurationPlatforms or other
        /// </summary>
        public string Kind
        {
            get;
            set;
        }

        /// <summary>
        /// Example preSolution or postSolution
        /// </summary>
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
