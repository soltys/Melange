using Soltys.Lisp.Compiler;
using Xunit;

namespace Soltys.Lisp.Test.Compiler
{
    public class LispParserTests
    {
        [Fact]
        public void ParseProgram_AddingTwoNumbers()
        {
            var expectedAst = new AstList(new IAstNode[] {
                new AstSymbol("add"),
                new AstIntNumber(2),
                new AstIntNumber(3),
            });

            AstAssert.Program(expectedAst, "(add 2 3)");
        }

        [Fact]
        public void ParseProgram_ParsingListInList()
        {
            var expectedAst = new AstList(new IAstNode[] {
                new AstSymbol("add"),
                new AstIntNumber(2),
                new AstList(new IAstNode[] {
                    new AstSymbol("mul"),
                    new AstIntNumber(3),
                    new AstIntNumber(4),
                }),
            });

            AstAssert.Program(expectedAst, "(add 2 (mul 3 4))");
        }

        [Fact]
        public void ParseProgram_ParsingMathOperation()
        {
            var expectedAst = new AstList(new IAstNode[] {
                new AstSymbol("-"),
                new AstList(new IAstNode[] {
                    new AstSymbol("+"),
                    new AstIntNumber(5),
                    new AstList(new IAstNode[] {
                        new AstSymbol("*"),
                        new AstIntNumber(2),
                        new AstIntNumber(3),
                    }),
                }),
                new AstIntNumber(3),
            });
            AstAssert.Program(expectedAst, "(- (+ 5 (* 2 3)) 3)");
        }

        [Fact]
        public void ParseProgram_WithNegativeNumbers()
        {
            var expectedAst = new AstList(new IAstNode[] {
                new AstSymbol("*"),
                new AstIntNumber(-3),
                new AstIntNumber(6),
            });
            AstAssert.Program(expectedAst, "(* -3 6)");
        }

        [Fact]
        public void ParseProgram_WithStringValues()
        {
            var expectedAst = new AstList(new IAstNode[] {
                new AstSymbol("strcat"),
                new AstString("foo"),
                new AstString("bar"),
            });
            AstAssert.Program(expectedAst, "(strcat \"foo\" \"bar\")");
        }

        [Fact]
        public void ParseProgram_WithDoubleValues()
        {
            var expectedAst = new AstList(new IAstNode[] {
                new AstSymbol("+"),
                new AstDoubleNumber(0.1),
                new AstDoubleNumber(0.2),
            });
            AstAssert.Program(expectedAst, "(+ 0.1 0.2)");
        }

        [Fact]
        public void ParseProgram_WithQuote()
        {
            var expectedAst = new AstList(
                    new AstSymbol("quote"), new AstList(
                        new AstIntNumber(1),
                        new AstIntNumber(2),
                        new AstIntNumber(3)
                    )
                );
            AstAssert.Program(expectedAst, "'(1 2 3)");
        }
    }
}
