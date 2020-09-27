using System;

namespace Soltys.Library.TextAnalysis
{
    [AttributeUsage(AttributeTargets.Field)]
    public class OperatorAttribute : Attribute
    {
        public Associativity Associativity { get; set; } = Associativity.Left;
        public int Precedence { get; set; }

    }
}
