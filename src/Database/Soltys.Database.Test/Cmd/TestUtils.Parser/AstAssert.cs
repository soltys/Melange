using Soltys.Library.TextAnalysis;

namespace Soltys.Database.Test.Cmd;

internal static class AstAssert
{
    public static void Statement(IAstNode expectedNode, string input) =>
        AssertAst(expectedNode, (IAstNode)ParserFactory(input).ParseStatement());
    public static void Expression(IAstNode expectedAst, string input) =>
        AssertAst(expectedAst, (IAstNode)ParserFactory(input).ParseExpression());

    public static void Factor(IAstNode expectedAst, string input) =>
        AssertAst(expectedAst, (IAstNode)ParserFactory(input).ParseFactor());

    private static void AssertAst(IAstNode expectedAst, IAstNode actualAst) =>
        new TestParserVisitor().AssertVisit(expectedAst, actualAst);

    private static Parser ParserFactory(string input) =>
        new Parser(
            new TokenSource<CmdToken, CmdTokenKind>(
                new Lexer(
                    new TextSource(input))));
}