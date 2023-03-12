using Soltys.Database.Test.TestUtils;
using Xunit;

namespace Soltys.Database.Test.Cmd;

internal class InsensitiveKeywordGenerator : TheoryData<InputTokenTypePair>
{
    public InsensitiveKeywordGenerator()
    {
        var dataList = EnumHelper.GetKeywords<CmdTokenKind>()
            .Select(x => new InputTokenTypePair
            {
                ExpectedCmdTokenKind = x,
                Input = x.ToString().ToLowerInvariant()
            });

        foreach (var pair in dataList)
        {
            Add(new InputTokenTypePair() { ExpectedCmdTokenKind = pair.ExpectedCmdTokenKind, Input = pair.Input });
            var newInput = char.ToUpperInvariant(pair.Input[0]) + pair.Input.Substring(1);
            Add(new InputTokenTypePair() { ExpectedCmdTokenKind = pair.ExpectedCmdTokenKind, Input = newInput });
            newInput = pair.Input.ToUpperInvariant();
            Add(new InputTokenTypePair() { ExpectedCmdTokenKind = pair.ExpectedCmdTokenKind, Input = newInput });
        }

    }
}

internal class InputTokenTypePair
{
    public CmdTokenKind ExpectedCmdTokenKind { get; set; }
    public string Input { get; set; }

    public override string ToString() => $"{Input} => CmdToken: {ExpectedCmdTokenKind}";
}
