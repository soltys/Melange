using System;

namespace Soltys.Library.VisualStudioSolution
{
    public class AstHeader
    {
        public AstHeader(string fileFormat, string iconVersion, string latestVisualStudioAccess, string minimumVisualStudio)
        {
            FileFormat = fileFormat;
            IconVersion = iconVersion;
            LatestVisualStudioAccess = latestVisualStudioAccess;
            MinimumVisualStudioVersion = minimumVisualStudio;
        }

        /// <summary>
        /// Example: Microsoft Visual Studio Solution File, Format Version 12.00
        /// </summary>
        public string FileFormat
        {
            get;
            set;
        }

        /// <summary>
        /// Example: # Visual Studio Version 16
        /// </summary>
        public string IconVersion
        {
            get;
            set;
        }

        /// <summary>
        /// Example: VisualStudioVersion = 16.0.28701.123
        /// </summary>
        public string LatestVisualStudioAccess
        {
            get;
            set;
        }

        /// <summary>
        /// Example: MinimumVisualStudioVersion = 10.0.40219.1
        /// </summary>
        public string MinimumVisualStudioVersion
        {
            get;
            set;
        }
    }
}
