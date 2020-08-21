using System.Xml.Serialization;

namespace SoltysDb.Core.Test.CmdCompiler
{
    [XmlRoot("LexerTestPlan", Namespace = "http://github.com/soltys/SoltysDB/LexerTestCases.xsd")]
    public class LexerTestPlan
    {
        [XmlElement("TestCase")]
        public LexerTestCase[] TestCases { get; set; }
    }
}