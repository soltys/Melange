using System.Linq;
using System.Xml.Serialization;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class LexerTestCase : ILexerTestCase
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("isBreakpointOn")]
        public bool IsBreakpointOn { get; set; }

        [XmlElement("Input")]
        public string Input { get; set; }

        [XmlArray("ExpectedTokens"), XmlArrayItem("Token")]
        public TestCaseToken[] ExpectedTokens { get; set; }

        Token[] ILexerTestCase.ExpectedTokens => ExpectedTokens.Select(x => x.ToToken()).ToArray();

        public override string ToString() => Name;
    }

    internal interface ILexerTestCase
    {
        bool IsBreakpointOn { get; }
        string Input { get; }
        Token[] ExpectedTokens { get; }
    }
}