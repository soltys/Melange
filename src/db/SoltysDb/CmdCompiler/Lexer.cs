using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SoltysDb
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
                    var nextChar = GetNextChar(i, input);
                    if (nextChar == '=')
                    {
                        yield return new Token(TokenType.CompareEqual, "==");
                        i += 1;
                    }
                    else
                    {
                        yield return new Token(TokenType.EqualSign, "=");
                    }
                }
               
                else if (currentChar == '<')
                {
                    var nextChar = GetNextChar(i, input);
                    if (nextChar == '=')
                    {
                        yield return new Token(TokenType.LessThanEqual, "<=");
                        i += 1;
                    }
                    else
                    {
                        yield return new Token(TokenType.LessThan, "<");
                    }
                }
                else if (currentChar == '>')
                {
                    var nextChar = GetNextChar(i, input);
                    if (nextChar == '=')
                    {
                        yield return new Token(TokenType.GreaterThanEqual, ">=");
                        i += 1;
                    }
                    else
                    {
                        yield return new Token(TokenType.GreaterThan, ">");
                    }
                }
                
                else if (currentChar == '+')
                {
                    yield return new Token(TokenType.Plus, "+");
                }
                else if (currentChar == '-')
                {
                    yield return new Token(TokenType.Minus, "-");
                }
                else if (currentChar == '/')
                {
                    yield return new Token(TokenType.Slash, "/");
                }
                else if (currentChar == '*')
                {
                    yield return new Token(TokenType.Star, "*");
                }
                else if (currentChar == '(')
                {
                    yield return new Token(TokenType.LParen, "(");
                }
                else if (currentChar == ')')
                {
                    yield return new Token(TokenType.RParen, ")");
                }
                else if (currentChar == ',')
                {
                    yield return new Token(TokenType.Comma, ",");
                }
                else if (currentChar == '.')
                {
                    yield return new Token(TokenType.Dot, ".");
                }
                else if(currentChar == '!')
                {
                    var nextChar = GetNextChar(i, input);
                    if (nextChar == '=')
                    {
                        yield return new Token(TokenType.CompareNotEqual, "!=");
                        i += 1;
                    }
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
                    else if(currentChar == '\"')
                    {
                        var match = Regex.Match(input.AsSpan().Slice(i).ToString(), "\"([^\"]*)\"", RegexOptions.Compiled);
                        yield return new Token(TokenType.String, match.Groups[1].Value);
                        i += match.Length;
                    }
                }
            }

        }

        private static char GetNextChar(int i, string input) => (i + 1 >= input.Length) ? '\0' : input[i + 1];

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

            while (i < slice.Length && IsNameCharacter(slice[i]))
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

            bool IsNameCharacter(char letter)
            {
                return char.IsLetterOrDigit(letter) ||
                       letter == '_';
            }
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
                case "and":
                    return new Token(TokenType.And, input);
                case "or":
                    return new Token(TokenType.Or, input);
                case "values":
                    return new Token(TokenType.Values, input);
                default:
                    return new Token(TokenType.Id, input);
            }
        }
    }
}
