using Soltys.Library.Bson;

namespace Soltys.Library.BQuery;

internal interface IBQueryExecutor
{
    BsonValue ExecuteValueQuery(BsonDocument document, AstValueAccess query);
}
