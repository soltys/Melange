using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SoltysDb.Core.Test.TestUtils
{
    public static class AssemblyExtensions
    {
        public static Stream FindFileStream(this Assembly assembly, string fileName)
        {
            var names = assembly.GetManifestResourceNames();
            var foundResources = names.Where(x => x.EndsWith(fileName)).ToArray();

            if (foundResources.Length == 0)
            {
                throw new ArgumentException($"No resources found under that name: {fileName}", nameof(fileName));
            }

            if (foundResources.Length > 1)
            {
                throw new ArgumentException($"Too many resources found under that name: {fileName}", nameof(fileName));
            }

            return assembly.GetManifestResourceStream(foundResources[0]);
        }
    }
}
