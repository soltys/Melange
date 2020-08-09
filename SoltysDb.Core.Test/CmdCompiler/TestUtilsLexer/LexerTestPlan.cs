using System.Xml.Serialization;

namespace SoltysDb.Core.Test.CmdCompiler
{
    [XmlRoot("LexerTestPlan")]
    public class LexerTestPlan
    {
        [XmlElement("TestCase")]
        public LexerTestCase[] TestCases { get; set; }
    }
}