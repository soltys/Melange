using System;

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
        GreaterThan,

        Plus,
        Minus,

        Star,
        Slash,

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
        From,
        
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class KeywordAttribute : Attribute
    {

    }
}