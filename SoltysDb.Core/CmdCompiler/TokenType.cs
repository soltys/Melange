namespace SoltysDb.Core
{
    internal enum TokenType
    {
        Undefined,
        
        //Common
        Id,
        Number,
        String,

        //Operators
        EqualSign,

        //Keywords
        Insert,
        Into,
    }
}