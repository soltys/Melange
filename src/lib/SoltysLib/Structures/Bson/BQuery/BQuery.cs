using SoltysLib.TextAnalysis;

namespace SoltysLib.Bson.BQuery
{
    public static class BQuery
    {
        public static BsonValue QueryValue(this BsonDocument document, string query)
        {
            var lexer = new BQueryLexer(query);
            var parser = new BQueryParser(new TokenSource<BQueryToken>(lexer));
            var ast = parser.ParseValueQuery();
            var executor = new BQueryExecutor();
            return executor.ExecuteValueQuery(document, ast);
        }
    }
}
