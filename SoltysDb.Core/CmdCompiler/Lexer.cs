using System;
using System.Collections.Generic;
using System.Text;

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
                if (input[i] == '=')
                {
                    yield return new Token(TokenType.EqualSign, "=");
                }
                else
                {
                    if (char.IsLetter(input[i]))
                    {
                        var (ident, offset) = GetIdent(input.AsSpan().Slice(i));
                        //We are checking if Ident is not special keyword
                        yield return MakeToken(ident);
                        
                        i += offset;
                    }
                }
            }

        }

        private (string, int) GetIdent(ReadOnlySpan<char> slice)
        {
            var builder = new StringBuilder();
            int i = 0;

            while (i < slice.Length && char.IsLetterOrDigit(slice[i]))
            {
                builder.Append(slice[i]);
                i++;
            }

            return (builder.ToString(), i-1);
        }

        private Token MakeToken(string input)
        {
            switch (input)
            {
                case "insert":
                    return new Token(TokenType.Insert, input);
                case "into":
                    return new Token(TokenType.Into, input);
                default:
                    return new Token(TokenType.Id, input);
            }
        }
    }
}
