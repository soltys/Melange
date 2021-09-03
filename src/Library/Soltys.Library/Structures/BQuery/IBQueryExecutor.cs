namespace Soltys.Library.Bson.BQuery
{
    internal interface IBQueryExecutor
    {
        BsonValue ExecuteValueQuery(BsonDocument document, AstValueAccess query);
    }
}
