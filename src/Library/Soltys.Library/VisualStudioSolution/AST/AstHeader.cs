using System;

namespace Soltys.Library.VisualStudioSolution
{
    public class AstHeader
    {
        public AstHeader(Version fileFormat, Version iconVersion, Version latestVisualStudioAccess, Version minimumVisualStudio)
        {
            FileFormat = fileFormat;
            IconVersion = iconVersion;
            LatestVisualStudioAccess = latestVisualStudioAccess;
            MinimumVisualStudioVersion = minimumVisualStudio;
        }

        /// <summary>
        /// Example: Microsoft Visual Studio Solution File, Format Version 12.00
        /// </summary>
        public Version FileFormat
        {
            get;
            set;
        }

        /// <summary>
        /// Example: # Visual Studio Version 16
        /// </summary>
        public Version IconVersion
        {
            get;
            set;
        }

        /// <summary>
        /// Example: VisualStudioVersion = 16.0.28701.123
        /// </summary>
        public Version LatestVisualStudioAccess
        {
            get;
            set;
        }

        /// <summary>
        /// Example: MinimumVisualStudioVersion = 10.0.40219.1
        /// </summary>
        public Version MinimumVisualStudioVersion
        {
            get;
            set;
        }
    }
}
