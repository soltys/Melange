using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Soltys.Library.Test.VisualStudioSolution.TestUtils
{
    internal class ExampleSolutionProvider
    {
        /// <summary>
        /// MSDN sample from from: https://docs.microsoft.com/en-us/visualstudio/extensibility/internals/solution-dot-sln-file?view=vs-2019
        /// </summary>
        public static string MsdnSample => 
            GetFileContentAsString("MsdnSolution.sln");

        private static string GetFileContentAsString(string resourceName)
        {
            var resourceStream =
                typeof(ExampleSolutionProvider).Assembly.GetManifestResourceStream(typeof(ExampleSolutionProvider),
                    resourceName);
            using var reader = new StreamReader(resourceStream);
            return reader.ReadToEnd();
        }
    }
}
