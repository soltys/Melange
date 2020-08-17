using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using SoltysDb.Core.Test.TestUtils;

namespace SoltysDb.Core.Test.CmdCompiler
{
    class LexerTestsCasesProvider : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var assembly = typeof(LexerTestsCasesProvider).GetTypeInfo().Assembly;
            XmlSerializer serializer = new XmlSerializer(typeof(LexerTestPlan));
            var plan = (LexerTestPlan)serializer.Deserialize(assembly.FindFileStream("LexerTestCases.xml"));

            foreach (var testCase in plan.TestCases)
            {
                yield return new object[] { testCase };
            }

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}