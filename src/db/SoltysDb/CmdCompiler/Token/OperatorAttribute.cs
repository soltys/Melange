using System;

namespace SoltysDb
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class OperatorAttribute : Attribute
    {
        public Associativity Associativity { get; set; } = Associativity.Left;
        public int Precedence { get; set; }

    }
}
