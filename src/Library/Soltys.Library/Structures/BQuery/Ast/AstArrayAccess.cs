namespace Soltys.Library.BQuery;

internal class AstArrayAccess :AstValueAccess
{
    public int ArrayIndex
    {
        get;
    }

    public AstArrayAccess(string elementName, int arrayIndex) : base(elementName)
    {
        ArrayIndex = arrayIndex;
    }
}
