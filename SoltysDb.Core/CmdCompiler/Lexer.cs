using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SoltysDb.Core
{
    internal class Lexer : ILexer
    {
        private readonly ICommandInput commandInput;

        public Lexer(ICommandInput commandInput)
        {
            this.commandInput = commandInput;
        }

        public IEnumerable<Token> GetTokens()
        {
            var input = this.commandInput.GetToEnd();

            for (int i = 0; i < input.Length; i++)
            {
                var currentChar = input[i];
                if (currentChar == '=')
                {
                    yield return new Token(TokenType.EqualSign, "=");
                }
                else if (currentChar == '*')
                {
                    yield return new Token(TokenType.Star, "*");
                }
                else if (currentChar == '>')
                {
                    yield return new Token(TokenType.GreaterThan, ">");
                }
                else if (currentChar == '+')
                {
                    yield return new Token(TokenType.Plus, "+");
                }
                else if (currentChar == '-')
                {
                    yield return new Token(TokenType.Minus, "-");
                }
                else if (currentChar == '(')
                {
                    yield return new Token(TokenType.LParen, "(");
                }
                else if (currentChar == ')')
                {
                    yield return new Token(TokenType.RParen, ")");
                }
                else if (currentChar == '/')
                {
                    yield return new Token(TokenType.Slash, "/");
                }
                else
                {
                    if (char.IsLetter(currentChar))
                    {
                        var (ident, offset) = GetAnyName(input.AsSpan().Slice(i));
                        yield return MakeToken(ident);

                        i += offset;
                    }
                    else if (char.IsDigit(currentChar))
                    {
                        var (token, offset) = GetNumber(input.AsSpan().Slice(i));
                        yield return token;
                        i += offset;
                    }
                    else if (currentChar == '\'')
                    {
                        var match = Regex.Match(input.AsSpan().Slice(i).ToString(), "\'([^\']*)\'", RegexOptions.Compiled);
                        yield return new Token(TokenType.String, match.Groups[1].Value);
                        i += match.Length;
                    }
                }
            }

        }

        private (Token, int) GetNumber(ReadOnlySpan<char> slice)
        {
            var builder = new StringBuilder();
            int i = 0;

            while (i < slice.Length && (char.IsDigit(slice[i]) || slice[i] == '.'))
            {
                builder.Append(slice[i]);
                i++;
            }

            return (new Token(TokenType.Number, builder.ToString()), i - 1);
        }


        private (string, int) GetAnyName(ReadOnlySpan<char> slice)
        {
            var builder = new StringBuilder();
            int i = 0;

            while (i < slice.Length && char.IsLetterOrDigit(slice[i]))
            {
                builder.Append(slice[i]);
                i++;
            }

            int offset = i - 1;

            if (offset < 0)
            {
                throw new InvalidOperationException("Something went wrong on here!");
            }

            return (builder.ToString(), i - 1);
        }

        private Token MakeToken(string input)
        {
            //we are testing lower case to make it case insensitive but we are preserving casing in Token Value
            var testString = input.ToLowerInvariant();
            switch (testString)
            {
                case "insert":
                    return new Token(TokenType.Insert, input);
                case "into":
                    return new Token(TokenType.Into, input);
                case "select":
                    return new Token(TokenType.Select, input);
                case "where":
                    return new Token(TokenType.Where, input);
                case "from":
                    return new Token(TokenType.From, input);
                default:
                    return new Token(TokenType.Id, input);
            }
        }
    }
}
