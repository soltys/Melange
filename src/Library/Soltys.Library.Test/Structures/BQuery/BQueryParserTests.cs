using Soltys.Library.BQuery;
using Soltys.Library.TextAnalysis;
using Xunit;

namespace Soltys.Library.Test;

public class BQueryParserTests
{
    [Theory]
    [InlineData("foo")]
    [InlineData("['foo']")]
    [InlineData("[\"foo\"]")]
    public void ParseAccessQuery_OnePart_ExpectedAst(string query)
    {
        var expectedAst = new AstValueAccess("foo");
        var actualAst = GetAccessQueryAst(query);

        Assert.Equal(expectedAst.ElementName, actualAst.ElementName);
        Assert.Null(actualAst.SubAccess);
    }

    [Theory]
    [InlineData("foo.bar")]
    [InlineData("['foo'].['bar']")]
    [InlineData("[\"foo\"].['bar']")]
    public void ParseAccessQuery_WithSubAccess_ExpectedAst(string query)
    {
        var expectedAst = new AstValueAccess("foo")
        {
            SubAccess = new AstValueAccess("bar")
        };

        var actualAst = GetAccessQueryAst(query);

        Assert.Equal(expectedAst.ElementName, actualAst.ElementName);
        Assert.Equal(expectedAst.SubAccess.ElementName, actualAst.SubAccess.ElementName);
    }

    [Fact]
    public void ParseAccessQuery_WithArrayAccess_ExpectedAst()
    {
        var expectedAst = new AstArrayAccess("foo", 42);
        var actualAst = GetAccessQueryAst("foo[42]");

        Assert.IsType<AstArrayAccess>(actualAst);
        var actualAccessAst = (AstArrayAccess)actualAst;
        Assert.Equal(expectedAst.ElementName, actualAccessAst.ElementName);
        Assert.Equal(expectedAst.ArrayIndex, actualAccessAst.ArrayIndex);
        Assert.Null(actualAccessAst.SubAccess);
    }

    [Fact]
    public void ParseAccessQuery_WithArrayAccessAndSubAccess_ExpectedAst()
    {
        var expectedAst = new AstArrayAccess("foo", 42)
        {
            SubAccess = new AstValueAccess("bar")
        };

        var actualAst = GetAccessQueryAst("foo[42].bar");

        Assert.IsType<AstArrayAccess>(actualAst);
        var actualAccessAst = (AstArrayAccess)actualAst;
        Assert.Equal(expectedAst.ElementName, actualAccessAst.ElementName);
        Assert.Equal(expectedAst.ArrayIndex, actualAccessAst.ArrayIndex);
        Assert.Equal(expectedAst.SubAccess.ElementName, actualAccessAst.SubAccess.ElementName);
    }

    private static AstValueAccess GetAccessQueryAst(string input)
    {
        var parser = new BQueryParser(new TokenSource<BQueryToken, BQueryTokenKind>(new BQueryLexer(new TextSource(input))));
        return parser.ParseValueQuery();
    }
}
