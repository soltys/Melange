using System;
using System.Collections.Generic;
using Soltys.Library.TextAnalysis;
using Soltys.Lisp.Compiler;

namespace Soltys.Lisp
{
    public class SoltysLisp : IDisposable
    {
        private readonly LispEnv env;

        public SoltysLisp()
        {
            this.env = new LispEnv();
        }

        public object Do(string input)
        {
            var ast = GetAst(input);
            return this.env.Eval(ast);
        }

        private static IEnumerable<IAstNode> GetAst(string input)
        {
            var lexer = new LispLexer(new TextSource(input));
            var parser = new LispParser(new TokenSource<LispToken, LispTokenKind>(lexer));
            return parser.ParseProgram();
        }

        public void Dispose()
        {
        }
    }
}
