using System;
using System.Xml.Serialization;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class TestCaseToken
    {
        [XmlAttribute("type")]
        public TokenType Type { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        internal Token ToToken() => new Token(Type, Value);
    }
}