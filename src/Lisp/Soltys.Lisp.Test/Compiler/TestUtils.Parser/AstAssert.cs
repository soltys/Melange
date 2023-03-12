using Soltys.Library.TextAnalysis;
using Soltys.Lisp.Compiler;

namespace Soltys.Lisp.Test.Compiler;

internal class AstAssert
{
    public static void Program(IAstNode expectedNode, string input) =>
        AssertAst(expectedNode, ParserFactory(input).ParseProgram().Single());

    private static void AssertAst(IAstNode expectedAst, IAstNode actualAst) =>
        new TestParserVisitor().AssertVisit(expectedAst, actualAst);

    private static LispParser ParserFactory(string input) =>
        new LispParser(
            new TokenSource<LispToken, LispTokenKind>(
                new LispLexer(
                    new TextSource(input))));
}
