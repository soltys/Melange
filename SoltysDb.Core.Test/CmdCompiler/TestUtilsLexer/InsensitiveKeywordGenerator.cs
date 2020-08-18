﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SoltysDb.Core.Test.TestUtils;

namespace SoltysDb.Core.Test.CmdCompiler
{
    class InsensitiveKeywordGenerator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {

            var dataList = EnumHelper.GetKeywords<TokenType>()
                .Select(x => new InputTokenTypePair
                {
                    ExpectedTokenType = x,
                    Input = x.ToString().ToLowerInvariant()
                });

            foreach (var pair in dataList)
            {
                yield return new object[] { new InputTokenTypePair() { ExpectedTokenType = pair.ExpectedTokenType, Input = pair.Input } };
                var newInput = char.ToUpperInvariant(pair.Input[0]) + pair.Input.Substring(1);
                yield return new object[] { new InputTokenTypePair() { ExpectedTokenType = pair.ExpectedTokenType, Input = newInput } };
                newInput = pair.Input.ToUpperInvariant();
                yield return new object[] { new InputTokenTypePair() { ExpectedTokenType = pair.ExpectedTokenType, Input = newInput } };
            }

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class InputTokenTypePair
    {
        public TokenType ExpectedTokenType { get; set; }
        public string Input { get; set; }

        public override string ToString() => $"{Input} => Token: {ExpectedTokenType}";
    }
}
