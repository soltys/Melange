using System;

namespace SoltysDb
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

        [Operator(Precedence = 1)]
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

    [AttributeUsage(AttributeTargets.Field)]
    public class OperatorAttribute : Attribute
    {
        public Associativity Associativity { get; set; } = Associativity.Left;
        public int Precedence { get; set; }

    }

    public enum Associativity
    {
        Left,
        Right
    }
}
