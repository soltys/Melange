using System;
using System.Collections.Generic;
using System.Linq;

namespace SoltysDb
{
    internal class Parser
    {
        private readonly ITokenSource ts;

        public Parser(ITokenSource ts)
        {
            this.ts = ts;
        }

        private TokenType CurrentToken => this.ts.Current.TokenType;
        private TokenType NextToken => this.ts.PeekNextToken.TokenType;

        private void AdvanceToken(params TokenType[] tokens)
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
        private bool IsToken(params TokenType[] tokens) => tokens.Any(x => x == CurrentToken);
        private bool IsNextToken(params TokenType[] tokens) => tokens.Any(x => x == NextToken);

        private void AdvanceIfToken(params TokenType[] tokens)
        {
            if (IsToken(tokens))
            {
                AdvanceToken(tokens);
            }
        }

        public AstNode ParseExpression() => ParseExpression(0);

        private readonly TokenType[] binaryTokens = new[] {
            TokenType.Plus, TokenType.Minus, TokenType.Star, TokenType.Slash, 
            //boolean operators
            TokenType.CompareEqual, TokenType.CompareNotEqual, TokenType.And, TokenType.Or,
            TokenType.GreaterThan, TokenType.GreaterThanEqual, TokenType.LessThan, TokenType.LessThanEqual
        };

        public IAstNode ParseStatement()
        {
            return CurrentToken switch
            {
                TokenType.Select => ParseSelect(),
                TokenType.Insert => ParseInsert(),
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
            AdvanceToken(TokenType.Insert);
            AdvanceToken(TokenType.Into);

            var location = ParseLocation();

            AdvanceToken(TokenType.Values);

            var values = ParseValues();
            return new AstInsertStatement() { Location = location, Values = values };
        }

        private AstValue ParseValues()
        {
            AdvanceToken(TokenType.LParen);

            var expressions = new List<AstExpression>();
            while (!IsToken(TokenType.RParen))
            {
                if (IsToken(TokenType.Id))
                {
                    expressions.Add(new AstExpression()
                    {
                        Value = this.ts.Current.Value
                    });

                    AdvanceToken(TokenType.Id);
                }

                AdvanceIfToken(TokenType.Comma);
            }
            AdvanceToken(TokenType.RParen);
            return new AstValue(expressions.ToArray());
        }

        private AstLocation ParseLocation()
        {
            var location = "";
            do
            {
                AdvanceIfToken(TokenType.Dot);

                if (IsToken(TokenType.Id, TokenType.String))
                {
                    location = $"{location}{this.ts.Current.Value}.";
                    AdvanceToken(TokenType.Id, TokenType.String);
                }
                else
                {
                    throw new InvalidOperationException($"No expected location token {CurrentToken}");
                }
            } while (IsToken(TokenType.Dot));

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

            int GetNewPrecedence(TokenType op)
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
            if (IsToken(TokenType.LParen))
            {
                AdvanceToken(TokenType.LParen);
                var expression = ParseExpression();
                AdvanceToken(TokenType.RParen);
                return expression;
            }

            //unary operator
            if (IsToken(TokenType.Minus, TokenType.Plus))
            {
                var op = CurrentToken;
                AdvanceToken(TokenType.Minus, TokenType.Plus);
                var unaryExpression = new AstUnaryExpression()
                {
                    Operator = op,
                    Expression = (AstExpression)ParseFactor()
                };
                return unaryExpression;
            }

            // number
            if (IsToken(TokenType.Number))
            {
                var numberExpression = new AstNumberExpression() { Value = this.ts.Current.Value };
                AdvanceToken(TokenType.Number);
                return numberExpression;
            }

            //function call
            if (IsToken(TokenType.Id))
            {
                var id = this.ts.Current.Value;
                AdvanceToken(TokenType.Id);

                if (IsToken(TokenType.LParen))
                {
                    var arguments = new List<AstExpression>();
                    AdvanceToken(TokenType.LParen);

                    while (!IsToken(TokenType.RParen))
                    {
                        arguments.Add((AstExpression)ParseExpression());

                        AdvanceIfToken(TokenType.Comma);

                    }

                    // Adding arguments should happening here
                    AdvanceToken(TokenType.RParen);
                    var functionCall = new AstFunctionCallExpression(id, arguments.ToArray());
                    return functionCall;
                }
            }

            throw new InvalidOperationException($"{nameof(ParseFactor)} could not find expression");
        }


    }
}
