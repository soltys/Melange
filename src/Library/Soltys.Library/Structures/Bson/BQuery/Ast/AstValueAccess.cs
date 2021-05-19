namespace Soltys.Library.Bson.BQuery
{
    internal class AstValueAccess
    {
        public AstValueAccess(string elementName)
        {
            ElementName = elementName;
        }
        public string ElementName
        {
            get;
        }

        public AstValueAccess? SubAccess
        {
            get;
            set;
        }
    }
}
