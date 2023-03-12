using Soltys.Library.TextAnalysis;

namespace Soltys.Database;

internal readonly struct CmdToken : IToken<CmdTokenKind>
{
    public static CmdToken Empty = new CmdToken(CmdTokenKind.Undefined, "");
    public CmdToken(CmdTokenKind cmdTokenKind, string value)
    {
        TokenKind = cmdTokenKind;
        Value = value;
    }

    public CmdTokenKind TokenKind
    {
        get;
    }
    public string Value
    {
        get;
    }

    public override string ToString() => $"<{TokenKind},{Value}>";

    public static bool operator ==(CmdToken lhs, CmdToken rhs) => lhs.Equals(rhs);
    public static bool operator !=(CmdToken lhs, CmdToken rhs) => !(lhs == rhs);

    public override bool Equals(object obj) => obj is CmdToken token && Equals(token);

    public bool Equals(CmdToken other) => TokenKind == other.TokenKind && Value == other.Value;
    public override int GetHashCode() => HashCode.Combine((int)TokenKind, Value);
}
