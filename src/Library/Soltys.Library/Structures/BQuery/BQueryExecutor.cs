#nullable enable
using Soltys.Library.Bson;

namespace Soltys.Library.BQuery;

internal class BQueryExecutor : IBQueryExecutor
{
    public BsonValue ExecuteValueQuery(BsonDocument document, AstValueAccess query)
    {
        AstValueAccess? currentQuery = query;
        Element currentElement = default;

        while (currentQuery != null)
        {
            if (currentQuery is AstArrayAccess aa)
            {
                currentElement = document.Elements.First(x => x.Name == aa.ElementName);

                if (currentElement.Value is BsonArray bsonArray)
                {
                    var arrayValue = bsonArray[aa.ArrayIndex];

                    //we are repacking this value into element for rest code to work
                    currentElement = new Element(aa.ArrayIndex.ToString(), arrayValue);
                }

                if (currentElement.Value is BsonDocument newDocument)
                {
                    document = newDocument;
                }
                currentQuery = currentQuery.SubAccess;
            }
            else if (currentQuery is { } va)
            {
                currentElement = document.Elements.First(x => x.Name == va.ElementName);
                if (currentElement.Value is BsonDocument newDocument)
                {
                    document = newDocument;
                }
                currentQuery = currentQuery.SubAccess;
            }

        }

        return currentElement.Value;
    }
}
