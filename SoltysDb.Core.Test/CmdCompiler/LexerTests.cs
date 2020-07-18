using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Xunit;

namespace SoltysDb.Core.Test.CmdCompiler
{
    public class LexerTests
    {
        private readonly Func<string, Lexer> lexerFactory;

        public LexerTests()
        {
            this.lexerFactory = (input)
                => new Lexer(new CommandInput(input));
        }

        [Theory]
        [ClassData(typeof(LexerTestsCasesProvider))]
        public void GetTokens_RunAllTestCases(LexerTestCase testCase)
        {
            var lexer = this.lexerFactory(testCase.Input);
            var tokens = lexer.GetTokens().ToArray();

            var expectedTokens = testCase.ExpectedTokens.Select(x => x.ToToken()).ToArray();

            Assert.Equal(
                expectedTokens.Select(x=>x.TokenType),
                tokens.Select(x => x.TokenType));

            Assert.Equal(
                expectedTokens.Select(x=>x.Value),
                tokens.Select(x => x.Value));
        }
    }

    class LexerTestsCasesProvider: IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var assembly = typeof(LexerTestsCasesProvider).GetTypeInfo().Assembly;
            var resource = assembly.GetManifestResourceStream("SoltysDb.Core.Test.CmdCompiler.LexerTestsCases.xml");
            XmlSerializer serializer =new XmlSerializer(typeof(LexerTestPlan));

            var plan =  (LexerTestPlan) serializer.Deserialize(resource);

            foreach (var testCase in plan.TestCases)
            {
                yield return new object[] {testCase};
            }

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
    }

    [XmlRoot("LexerTestPlan")]
    public class LexerTestPlan
    {
        [XmlElement("TestCase")]
        public LexerTestCase[] TestCases { get; set; }
    }

    public class LexerTestCase
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Input")]
        public string Input { get; set; }

        [XmlArray("ExpectedTokens"), XmlArrayItem("Token")]
        public TestCaseToken[] ExpectedTokens { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

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
                return new Token((TokenType) type, Value);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Type is not valid token type: {Type}");
            }
            
        }
    }
}
