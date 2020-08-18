namespace SoltysDb.Core
{
    public enum TokenType
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