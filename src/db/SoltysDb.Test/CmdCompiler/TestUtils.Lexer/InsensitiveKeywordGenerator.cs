using System.Linq;
using SoltysDb.Test.TestUtils;
using Xunit;

namespace SoltysDb.Test.CmdCompiler
{
    class InsensitiveKeywordGenerator : TheoryData<InputTokenTypePair>
    {
        public InsensitiveKeywordGenerator()
        {
            var dataList = EnumHelper.GetKeywords<TokenType>()
                .Select(x => new InputTokenTypePair
                {
                    ExpectedTokenType = x,
                    Input = x.ToString().ToLowerInvariant()
                });

            foreach (var pair in dataList)
            {
                Add(new InputTokenTypePair() { ExpectedTokenType = pair.ExpectedTokenType, Input = pair.Input });
                var newInput = char.ToUpperInvariant(pair.Input[0]) + pair.Input.Substring(1);
                Add(new InputTokenTypePair() { ExpectedTokenType = pair.ExpectedTokenType, Input = newInput });
                newInput = pair.Input.ToUpperInvariant();
                Add(new InputTokenTypePair() { ExpectedTokenType = pair.ExpectedTokenType, Input = newInput });
            }

        }
    }

    internal class InputTokenTypePair
    {
        public TokenType ExpectedTokenType { get; set; }
        public string Input { get; set; }

        public override string ToString() => $"{Input} => Token: {ExpectedTokenType}";
    }
}
