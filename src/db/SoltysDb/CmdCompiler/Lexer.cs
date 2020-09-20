using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SoltysLib.TextAnalysis;

namespace SoltysDb
{
    internal class Lexer : ILexer<Token>
    {
        private readonly ITextSource textSource;

        public Lexer(ITextSource textSource)
        {
            this.textSource = textSource;
        }

        public Token Empty => Token.Empty;

        public IEnumerable<Token> GetTokens()
        {
            while (!this.textSource.IsEnded)
            {

                var currentChar = this.textSource.Current;
                if (currentChar == '=')
                {
                    var nextChar = this.textSource.Next;
                    if (nextChar == '=')
                    {
                        yield return new Token(TokenKind.CompareEqual, "==");
                        this.textSource.AdvanceChar();
                    }
                    else
                    {
                        yield return new Token(TokenKind.EqualSign, "=");
                    }
                }

                else if (currentChar == '<')
                {
                    var nextChar = this.textSource.Next;
                    if (nextChar == '=')
                    {
                        yield return new Token(TokenKind.LessThanEqual, "<=");
                        this.textSource.AdvanceChar();
                    }
                    else
                    {
                        yield return new Token(TokenKind.LessThan, "<");
                    }
                }
                else if (currentChar == '>')
                {
                    var nextChar = this.textSource.Next;
                    if (nextChar == '=')
                    {
                        yield return new Token(TokenKind.GreaterThanEqual, ">=");
                        this.textSource.AdvanceChar();
                    }
                    else
                    {
                        yield return new Token(TokenKind.GreaterThan, ">");
                    }
                }

                else if (currentChar == '+')
                {
                    yield return new Token(TokenKind.Plus, "+");
                }
                else if (currentChar == '-')
                {
                    yield return new Token(TokenKind.Minus, "-");
                }
                else if (currentChar == '/')
                {
                    yield return new Token(TokenKind.Slash, "/");
                }
                else if (currentChar == '*')
                {
                    yield return new Token(TokenKind.Star, "*");
                }
                else if (currentChar == '(')
                {
                    yield return new Token(TokenKind.LParen, "(");
                }
                else if (currentChar == ')')
                {
                    yield return new Token(TokenKind.RParen, ")");
                }
                else if (currentChar == ',')
                {
                    yield return new Token(TokenKind.Comma, ",");
                }
                else if (currentChar == '.')
                {
                    yield return new Token(TokenKind.Dot, ".");
                }
                else if (currentChar == '!')
                {
                    var nextChar = this.textSource.Next;
                    if (nextChar == '=')
                    {
                        yield return new Token(TokenKind.CompareNotEqual, "!=");
                        this.textSource.AdvanceChar();
                    }
                }
                else
                {
                    if (char.IsLetter(currentChar))
                    {
                        var (ident, offset) = GetAnyName(this.textSource.Slice());
                        yield return MakeToken(ident);
                        this.textSource.AdvanceChar(offset);
                    }
                    else if (char.IsDigit(currentChar))
                    {
                        var (token, offset) = GetNumber(this.textSource.Slice());
                        yield return token;
                        this.textSource.AdvanceChar(offset);
                    }
                    else if (currentChar == '\'')
                    {
                        var match = Regex.Match(this.textSource.Slice().ToString(), "\'([^\']*)\'", RegexOptions.Compiled);
                        yield return new Token(TokenKind.String, match.Groups[1].Value);
                        this.textSource.AdvanceChar(match.Length - 1);
                    }
                    else if (currentChar == '\"')
                    {
                        var match = Regex.Match(this.textSource.Slice().ToString(), "\"([^\"]*)\"", RegexOptions.Compiled);
                        yield return new Token(TokenKind.String, match.Groups[1].Value);
                        this.textSource.AdvanceChar(match.Length - 1);
                    }
                }

                this.textSource.AdvanceChar();
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

            return (new Token(TokenKind.Number, builder.ToString()), i - 1);
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
                    return new Token(TokenKind.Insert, input);
                case "into":
                    return new Token(TokenKind.Into, input);
                case "select":
                    return new Token(TokenKind.Select, input);
                case "where":
                    return new Token(TokenKind.Where, input);
                case "from":
                    return new Token(TokenKind.From, input);
                case "and":
                    return new Token(TokenKind.And, input);
                case "or":
                    return new Token(TokenKind.Or, input);
                case "values":
                    return new Token(TokenKind.Values, input);
                default:
                    return new Token(TokenKind.Id, input);
            }
        }
    }
}
