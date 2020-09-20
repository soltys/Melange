using SoltysLib.TextAnalysis;

namespace SoltysLib.Bson.BQuery
{
    public static class BQuery
    {
        public static BsonValue QueryValue(this BsonDocument document, string query)
        {
            var lexer = new BQueryLexer(new TextSource(query));
            var parser = new BQueryParser(new TokenSource<BQueryToken, BQueryTokenKind>(lexer));
            var ast = parser.ParseValueQuery();
            var executor = new BQueryExecutor();
            return executor.ExecuteValueQuery(document, ast);
        }
    }
}
