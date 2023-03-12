using Soltys.Library.TextAnalysis;

namespace Soltys.Library.BQuery;

internal readonly struct BQueryToken : IToken<BQueryTokenKind>
{
    public BQueryToken(BQueryTokenKind kind, string value) : this(kind, value, new Position())
    {
    }

    public BQueryToken(BQueryTokenKind kind, string value, Position position)
    {
        TokenKind = kind;
        Value = value;
        Position = position;
    }

    public BQueryTokenKind TokenKind
    {
        get;
    }

    public string Value
    {
        get;
    }

    public static BQueryToken Empty
    {
        get;

    } = new BQueryToken(BQueryTokenKind.Undefined, "", new Position(0, 0));

    public Position Position
    {
        get;
    }

    public override string ToString() => $"<{TokenKind}, {Value}>";

    public static bool operator ==(BQueryToken lhs, BQueryToken rhs) => lhs.Equals(rhs);
    public static bool operator !=(BQueryToken lhs, BQueryToken rhs) => !(lhs == rhs);

    public override bool Equals(object obj) => base.Equals(obj);
    public bool Equals(BQueryToken other) => TokenKind == other.TokenKind && Value == other.Value;
    public override int GetHashCode() => HashCode.Combine((int)TokenKind, Value);
}
