using System.Linq;
using Soltys.Database.Test.TestUtils;
using Xunit;

namespace Soltys.Database.Test.CmdCompiler
{
    internal class InsensitiveKeywordGenerator : TheoryData<InputTokenTypePair>
    {
        public InsensitiveKeywordGenerator()
        {
            var dataList = EnumHelper.GetKeywords<TokenKind>()
                .Select(x => new InputTokenTypePair
                {
                    ExpectedTokenKind = x,
                    Input = x.ToString().ToLowerInvariant()
                });

            foreach (var pair in dataList)
            {
                Add(new InputTokenTypePair() { ExpectedTokenKind = pair.ExpectedTokenKind, Input = pair.Input });
                var newInput = char.ToUpperInvariant(pair.Input[0]) + pair.Input.Substring(1);
                Add(new InputTokenTypePair() { ExpectedTokenKind = pair.ExpectedTokenKind, Input = newInput });
                newInput = pair.Input.ToUpperInvariant();
                Add(new InputTokenTypePair() { ExpectedTokenKind = pair.ExpectedTokenKind, Input = newInput });
            }

        }
    }

    internal class InputTokenTypePair
    {
        public TokenKind ExpectedTokenKind { get; set; }
        public string Input { get; set; }

        public override string ToString() => $"{Input} => Token: {ExpectedTokenKind}";
    }
}
