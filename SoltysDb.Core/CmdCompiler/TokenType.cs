﻿using System;

namespace SoltysDb.Core
{
    public enum TokenType
    {
        Undefined,
        
        LParen,
        RParen,

        //Common
        Id,
        Number,
        String,

        //Operators
        EqualSign,
        Star,
        GreaterThan,
        Plus,
        Minus,

        //Keywords
        [Keyword]
        Select,

        [Keyword]
        Where,

        [Keyword]
        Insert,

        [Keyword]
        Into,

        [Keyword]
        From
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class KeywordAttribute : Attribute
    {

    }
}