namespace Soltys.Library.Bson.BQuery
{
    internal class AstValueAccess
    {
        public AstValueAccess(string elementName)
        {
            this.ElementName = elementName;
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
