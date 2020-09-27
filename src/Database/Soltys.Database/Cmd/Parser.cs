using System;
using System.Collections.Generic;
using Soltys.Library.TextAnalysis;

namespace Soltys.Database
{
    internal sealed class Parser : ParserBase<CmdToken, CmdTokenKind>
    {
        public Parser(ITokenSource<CmdToken, CmdTokenKind> ts) : base(ts)
        {
        }

        public AstNode ParseExpression() => ParseExpression(0);

        private readonly CmdTokenKind[] binaryTokens = new[] {
            CmdTokenKind.Plus, CmdTokenKind.Minus, CmdTokenKind.Star, CmdTokenKind.Slash, 
            //boolean operators
            CmdTokenKind.CompareEqual, CmdTokenKind.CompareNotEqual, CmdTokenKind.And, CmdTokenKind.Or,
            CmdTokenKind.GreaterThan, CmdTokenKind.GreaterThanEqual, CmdTokenKind.LessThan, CmdTokenKind.LessThanEqual
        };

        public IAstNode ParseStatement()
        {
            return CurrentToken switch
            {
                CmdTokenKind.Select => ParseSelect(),
                CmdTokenKind.Insert => ParseInsert(),
                _ => throw new InvalidOperationException($"{nameof(ParseFactor)} could not token matching statement")
            };
        }

        private IAstNode ParseSelect()
        {
            var statement = new AstSelectStatement();
            return null;
        }

        private IAstNode ParseInsert()
        {
            AdvanceToken(CmdTokenKind.Insert);
            AdvanceToken(CmdTokenKind.Into);

            var location = ParseLocation();

            AdvanceToken(CmdTokenKind.Values);

            var values = ParseValues();
            return new AstInsertStatement() { Location = location, Values = values };
        }

        private AstValue ParseValues()
        {
            AdvanceToken(CmdTokenKind.LParen);

            var expressions = new List<AstExpression>();
            while (!IsToken(CmdTokenKind.RParen))
            {
                if (IsToken(CmdTokenKind.Id))
                {
                    expressions.Add(new AstExpression()
                    {
                        Value = this.ts.Current.Value
                    });

                    AdvanceToken(CmdTokenKind.Id);
                }

                AdvanceIfToken(CmdTokenKind.Comma);
            }
            AdvanceToken(CmdTokenKind.RParen);
            return new AstValue(expressions.ToArray());
        }

        private AstLocation ParseLocation()
        {
            var location = "";
            do
            {
                AdvanceIfToken(CmdTokenKind.Dot);

                if (IsToken(CmdTokenKind.Id, CmdTokenKind.String))
                {
                    location = $"{location}{this.ts.Current.Value}.";
                    AdvanceToken(CmdTokenKind.Id, CmdTokenKind.String);
                }
                else
                {
                    throw new InvalidOperationException($"No expected location token {CurrentToken}");
                }
            } while (IsToken(CmdTokenKind.Dot));

            return new AstLocation() { Value = location.TrimEnd('.') };
        }

        private AstNode ParseExpression(int precedence)
        {
            var expression = ParseFactor();

            while (IsToken(this.binaryTokens) && CurrentToken.GetPrecedence() >= precedence)
            {
                var op = CurrentToken;
                AdvanceToken(op);

                var newPrecedence = GetNewPrecedence(op);

                var rightExpression = (AstExpression)ParseExpression(newPrecedence);

                expression = new AstBinaryExpression()
                {
                    Lhs = (AstExpression)expression,
                    Rhs = rightExpression,
                    Operator = op
                };
            }

            return expression;

            int GetNewPrecedence(CmdTokenKind op)
            {
                if (op.GetAssociativity() == Associativity.Right)
                {
                    return op.GetPrecedence();
                }

                return op.GetPrecedence() + 1;
            }
        }

        public AstNode ParseFactor()
        {
            // '(' expression ')'
            if (IsToken(CmdTokenKind.LParen))
            {
                AdvanceToken(CmdTokenKind.LParen);
                var expression = ParseExpression();
                AdvanceToken(CmdTokenKind.RParen);
                return expression;
            }

            //unary operator
            if (IsToken(CmdTokenKind.Minus, CmdTokenKind.Plus))
            {
                var op = CurrentToken;
                AdvanceToken(CmdTokenKind.Minus, CmdTokenKind.Plus);
                var unaryExpression = new AstUnaryExpression()
                {
                    Operator = op,
                    Expression = (AstExpression)ParseFactor()
                };
                return unaryExpression;
            }

            // number
            if (IsToken(CmdTokenKind.Number))
            {
                var numberExpression = new AstNumberExpression() { Value = this.ts.Current.Value };
                AdvanceToken(CmdTokenKind.Number);
                return numberExpression;
            }

            //function call
            if (IsToken(CmdTokenKind.Id))
            {
                var id = this.ts.Current.Value;
                AdvanceToken(CmdTokenKind.Id);

                if (IsToken(CmdTokenKind.LParen))
                {
                    var arguments = new List<AstExpression>();
                    AdvanceToken(CmdTokenKind.LParen);

                    while (!IsToken(CmdTokenKind.RParen))
                    {
                        arguments.Add((AstExpression)ParseExpression());

                        AdvanceIfToken(CmdTokenKind.Comma);

                    }

                    // Adding arguments should happening here
                    AdvanceToken(CmdTokenKind.RParen);
                    var functionCall = new AstFunctionCallExpression(id, arguments.ToArray());
                    return functionCall;
                }
            }

            throw new InvalidOperationException($"{nameof(ParseFactor)} could not find expression");
        }
    }
}
