using Soltys.Library.TextAnalysis;

namespace Soltys.Lisp.Compiler;

internal readonly struct LispToken : IToken<LispTokenKind>
{
    public LispToken(LispTokenKind lispTokenKind, string value)
    {
        TokenKind = lispTokenKind;
        Value = value;
        Position = Position.Empty;
    }

    public LispToken(LispTokenKind lispTokenKind, string value, Position position)
    {
        TokenKind = lispTokenKind;
        Value = value;
        Position = position;
    }

    public LispTokenKind TokenKind
    {
        get;
    }

    public string Value
    {
        get;
    }

    public Position Position
    {
        get;
    }

    public static LispToken Empty
    {
        get;
    } = new LispToken(LispTokenKind.Undefined, string.Empty, Position.Empty);

    public override string ToString() => $"<{TokenKind}, {Value}>";

    public static bool operator ==(LispToken lhs, LispToken rhs) => lhs.Equals(rhs);
    public static bool operator !=(LispToken lhs, LispToken rhs) => !(lhs == rhs);

    public override bool Equals(object obj) => base.Equals(obj);
    public bool Equals(LispToken other) => TokenKind == other.TokenKind && Value == other.Value;
    public override int GetHashCode() => HashCode.Combine((int)TokenKind, Value);
}
