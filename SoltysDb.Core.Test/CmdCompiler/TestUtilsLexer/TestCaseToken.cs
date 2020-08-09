using System;
using System.Xml.Serialization;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class TestCaseToken
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        internal Token ToToken()
        {
            if (Enum.TryParse(typeof(TokenType), Type, false, out var type))
            {
                return new Token((TokenType)type, Value);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Type is not valid token type: {Type}");
            }
        }
    }
}