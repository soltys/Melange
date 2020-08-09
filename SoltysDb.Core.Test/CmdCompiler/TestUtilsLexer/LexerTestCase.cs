using System.Xml.Serialization;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class LexerTestCase
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("isBreakpointOn")]
        public bool IsBreakpointOn { get; set; }

        [XmlElement("Input")]
        public string Input { get; set; }

        [XmlArray("ExpectedTokens"), XmlArrayItem("Token")]
        public TestCaseToken[] ExpectedTokens { get; set; }

        public override string ToString() => Name;
    }
}