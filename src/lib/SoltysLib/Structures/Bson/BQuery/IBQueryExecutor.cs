namespace SoltysLib.Bson.BQuery
{
    internal interface IBQueryExecutor
    {
        BsonValue ExecuteValueQuery(BsonDocument document, AstValueAccess query);
    }
}
