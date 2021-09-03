using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Soltys.Library.TextAnalysis
{
    public class LexerHelper
    {
        public static (string, int) GetDoubleQuoteString(ReadOnlySpan<char> span)
        {
            var match = Regex.Match(span.ToString(), "\"([^\"]*)\"", RegexOptions.Compiled);
            return (match.Groups[1].Value, match.Length - 1);
        }

        public static (string, int) GetSingleQuoteString(ReadOnlySpan<char> span)
        {
            var match = Regex.Match(span.ToString(), "'([^']*)'", RegexOptions.Compiled);
            return (match.Groups[1].Value, match.Length - 1);
        }

        public static (string, int) GetAnyName(ReadOnlySpan<char> slice)
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

        public static (string, int) GetNumber(ReadOnlySpan<char> slice)
        {
            var builder = new StringBuilder();
            int i = 0;

            while (i < slice.Length && (char.IsDigit(slice[i]) || slice[i] == '.'))
            {
                builder.Append(slice[i]);
                i++;
            }

            return (builder.ToString(), i - 1);
        }
    }
}
