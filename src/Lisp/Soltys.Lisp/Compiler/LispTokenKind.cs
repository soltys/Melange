namespace Soltys.Lisp.Compiler;

internal enum LispTokenKind
{
    ///<summary> something went wrong</summary>
    Undefined,
    ///<summary>e.g. (</summary>
    LParen,
    ///<summary>e.g. )</summary>
    RParen,
    ///<summary>e.g. foo-bar</summary>
    Symbol,
    ///<summary>e.g. 420</summary>
    Number,
    ///<summary>e.g. "foobar"</summary>
    String,
    ///<summary>e.g. '</summary>
    Quote
}