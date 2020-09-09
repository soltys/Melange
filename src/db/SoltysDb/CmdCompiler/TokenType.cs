using System;

namespace SoltysDb
{
    public enum TokenType
    {
        Undefined,
        
        LParen,
        RParen,
        Comma,
        Dot,

        //Common
        Id,
        Number,
        String,

        //Operators
        EqualSign,
        [Operator(Precedence = 1)]
        CompareEqual,
        [Operator(Precedence = 1)]
        CompareNotEqual,
        [Operator(Precedence = 1)]
        GreaterThan,
        [Operator(Precedence = 1)]
        GreaterThanEqual,
        [Operator(Precedence = 1)]
        LessThan,
        [Operator(Precedence = 1)]
        LessThanEqual,

        [Operator(Precedence = 2)]
        Plus,
        [Operator(Precedence = 2)]
        Minus,

        [Operator(Precedence = 3)]
        Star,
        [Operator(Precedence = 3)]
        Slash,
        [Operator(Precedence = 4, Associativity = Associativity.Right)]
        Power,

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

        [Operator(Precedence = 1)]
        [Keyword]
        And,

        [Operator(Precedence = 1)]
        [Keyword]
        Or,

        [Keyword]
        Values,
     
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
