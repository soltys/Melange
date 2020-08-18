using System.Reflection;
using System.Xml.Serialization;
using SoltysDb.Core.Test.TestUtils;
using Xunit;

namespace SoltysDb.Core.Test.CmdCompiler
{
    class LexerTestsCasesProvider : TheoryData<LexerTestCase>
    {
        public LexerTestsCasesProvider()
        {
            var assembly = typeof(LexerTestsCasesProvider).GetTypeInfo().Assembly;
            XmlSerializer serializer = new XmlSerializer(typeof(LexerTestPlan));
            var plan = (LexerTestPlan)serializer.Deserialize(assembly.FindFileStream("LexerTestCases.xml"));
            
            foreach (var testCase in plan.TestCases)
            {
                Add(testCase);
            }
        }

    }
}