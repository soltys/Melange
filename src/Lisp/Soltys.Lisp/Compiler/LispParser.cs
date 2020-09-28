using System;
using System.Collections.Generic;
using System.Globalization;
using Soltys.Library.TextAnalysis;

namespace Soltys.Lisp.Compiler
{
    internal class LispParser : ParserBase<LispToken, LispTokenKind>
    {
        public LispParser(ITokenSource<LispToken, LispTokenKind> ts) : base(ts)
        {
        }

        public IEnumerable<IAstNode> ParseProgram() => ParseExpressionList();

        private IEnumerable<IAstNode> ParseExpressionList()
        {
            var expressionList = new List<IAstNode>();
            IAstNode? expr;
            while ((expr = ParseList()) != null)
            {
                expressionList.Add(expr);
            }

            return expressionList;
        }

        private IAstNode? ParseList()
        {
            if (!IsToken(LispTokenKind.LParen))
            {
                return null;
                // TODO: Maybe figure out how to not use null value as return
                //throw new InvalidOperationException("Expression should start with LParen");
            }

            AdvanceToken(LispTokenKind.LParen);

            var astList = new AstList();

            while (!IsToken(LispTokenKind.RParen))
            {
                astList.Add(ParseAtom());
            }

            AdvanceToken(LispTokenKind.RParen);

            return astList;

        }

        private IAstNode ParseAtom()
        {
            var token = this.ts.Current;
            switch (CurrentToken)
            {
                case LispTokenKind.Symbol:
                    AdvanceToken(LispTokenKind.Symbol);
                    return new AstSymbol(token.Value);
                case LispTokenKind.String:
                    AdvanceToken(LispTokenKind.String);
                    return new AstString(token.Value);
                case LispTokenKind.Number:
                    AdvanceToken(LispTokenKind.Number);
                    return ParseNumber(token.Value);
                case LispTokenKind.LParen:
                    return ParseList() ?? throw new InvalidOperationException("Expression should not be null");
                default:
                    throw new InvalidOperationException($"Unexpected token {token.TokenKind}");
            }
        }

        private static IAstNode ParseNumber(in string value)
        {
            if (int.TryParse(value, out var i))
            {
                return new AstIntNumber(i);
            }

            if (double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var d))
            {
                return new AstDoubleNumber(d);
            }

            throw new InvalidOperationException("Unexpected value while parsing number");
        }
    }
}
