﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SoltysDb.Core.Test.TestUtils
{
    class Generator
    {
        public static IEnumerable<KeyValuePair<string, string>> GenerateKeyValuesPairs(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new KeyValuePair<string, string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }
        }
    }
}