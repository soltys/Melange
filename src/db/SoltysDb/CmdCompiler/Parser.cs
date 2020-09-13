using System;
using System.Collections.Generic;
using System.Linq;
using SoltysLib.TextAnalysis;

namespace SoltysDb
{
    internal class Parser
    {
        private readonly ITokenSource<Token> ts;

        public Parser(ITokenSource<Token> ts)
        {
            this.ts = ts;
        }

        private TokenKind CurrentToken => this.ts.Current.TokenKind;
        private TokenKind NextToken => this.ts.PeekNextToken.TokenKind;

        private void AdvanceToken(params TokenKind[] tokens)
        {
            //
            //We are checking if current token is one of the expected ones
            //TODO: this could/should be disabled in final product
            if (tokens != null && !IsToken(tokens))
            {
                throw new InvalidOperationException("Not expected token advancement");
            }
            this.ts.NextToken();
        }
        private bool IsToken(params TokenKind[] tokens) => tokens.Any(x => x == CurrentToken);
        private bool IsNextToken(params TokenKind[] tokens) => tokens.Any(x => x == NextToken);

        private void AdvanceIfToken(params TokenKind[] tokens)
        {
            if (IsToken(tokens))
            {
                AdvanceToken(tokens);
            }
        }

        public AstNode ParseExpression() => ParseExpression(0);

        private readonly TokenKind[] binaryTokens = new[] {
            TokenKind.Plus, TokenKind.Minus, TokenKind.Star, TokenKind.Slash, 
            //boolean operators
            TokenKind.CompareEqual, TokenKind.CompareNotEqual, TokenKind.And, TokenKind.Or,
            TokenKind.GreaterThan, TokenKind.GreaterThanEqual, TokenKind.LessThan, TokenKind.LessThanEqual
        };

        public IAstNode ParseStatement()
        {
            return CurrentToken switch
            {
                TokenKind.Select => ParseSelect(),
                TokenKind.Insert => ParseInsert(),
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
            AdvanceToken(TokenKind.Insert);
            AdvanceToken(TokenKind.Into);

            var location = ParseLocation();

            AdvanceToken(TokenKind.Values);

            var values = ParseValues();
            return new AstInsertStatement() { Location = location, Values = values };
        }

        private AstValue ParseValues()
        {
            AdvanceToken(TokenKind.LParen);

            var expressions = new List<AstExpression>();
            while (!IsToken(TokenKind.RParen))
            {
                if (IsToken(TokenKind.Id))
                {
                    expressions.Add(new AstExpression()
                    {
                        Value = this.ts.Current.Value
                    });

                    AdvanceToken(TokenKind.Id);
                }

                AdvanceIfToken(TokenKind.Comma);
            }
            AdvanceToken(TokenKind.RParen);
            return new AstValue(expressions.ToArray());
        }

        private AstLocation ParseLocation()
        {
            var location = "";
            do
            {
                AdvanceIfToken(TokenKind.Dot);

                if (IsToken(TokenKind.Id, TokenKind.String))
                {
                    location = $"{location}{this.ts.Current.Value}.";
                    AdvanceToken(TokenKind.Id, TokenKind.String);
                }
                else
                {
                    throw new InvalidOperationException($"No expected location token {CurrentToken}");
                }
            } while (IsToken(TokenKind.Dot));

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

            int GetNewPrecedence(TokenKind op)
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
            if (IsToken(TokenKind.LParen))
            {
                AdvanceToken(TokenKind.LParen);
                var expression = ParseExpression();
                AdvanceToken(TokenKind.RParen);
                return expression;
            }

            //unary operator
            if (IsToken(TokenKind.Minus, TokenKind.Plus))
            {
                var op = CurrentToken;
                AdvanceToken(TokenKind.Minus, TokenKind.Plus);
                var unaryExpression = new AstUnaryExpression()
                {
                    Operator = op,
                    Expression = (AstExpression)ParseFactor()
                };
                return unaryExpression;
            }

            // number
            if (IsToken(TokenKind.Number))
            {
                var numberExpression = new AstNumberExpression() { Value = this.ts.Current.Value };
                AdvanceToken(TokenKind.Number);
                return numberExpression;
            }

            //function call
            if (IsToken(TokenKind.Id))
            {
                var id = this.ts.Current.Value;
                AdvanceToken(TokenKind.Id);

                if (IsToken(TokenKind.LParen))
                {
                    var arguments = new List<AstExpression>();
                    AdvanceToken(TokenKind.LParen);

                    while (!IsToken(TokenKind.RParen))
                    {
                        arguments.Add((AstExpression)ParseExpression());

                        AdvanceIfToken(TokenKind.Comma);

                    }

                    // Adding arguments should happening here
                    AdvanceToken(TokenKind.RParen);
                    var functionCall = new AstFunctionCallExpression(id, arguments.ToArray());
                    return functionCall;
                }
            }

            throw new InvalidOperationException($"{nameof(ParseFactor)} could not find expression");
        }


    }
}
