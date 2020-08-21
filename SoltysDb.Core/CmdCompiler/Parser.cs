namespace SoltysDb.Core
{
    internal class Parser
    {
        private readonly ITokenSource ts;

        public Parser(ITokenSource ts)
        {
            this.ts = ts;
        }

        private TokenType CurrentToken => this.ts.Current.TokenType;
        private TokenType NextToken => this.ts.Current.TokenType;
        private void GoToNextToken() => this.ts.NextToken();

        public AstNode ParseExpression()
        {
            var term = ParseTerm();

            if (CurrentToken == TokenType.Plus)
            {
                var leftExpression = (AstExpression)term;
                GoToNextToken();
                var rightExpression = (AstExpression)ParseExpression();

                var binaryExpression = new AstBinaryExpression()
                {
                    LeftExpression = leftExpression,
                    RightExpression = rightExpression,
                    Operator = TokenType.Plus
                };
                return binaryExpression;
            }

            return term;

        }

        public AstNode ParseTerm()
        {
            var factor = ParseFactor();
            if (CurrentToken == TokenType.Star)
            {
                var leftExpression = (AstExpression)factor;
                GoToNextToken();
                var rightExpression = (AstExpression)ParseTerm();

                var binaryExpression = new AstBinaryExpression()
                {
                    LeftExpression = leftExpression,
                    RightExpression = rightExpression,
                    Operator = TokenType.Star
                };
                return binaryExpression;
            }
            return factor;
        }

        public AstNode ParseFactor()
        {
            // '(' expression ')'
            if (CurrentToken == TokenType.LParen)
            {
                GoToNextToken();
                var expression = ParseExpression();
                GoToNextToken();
                return expression;
            }

            if (CurrentToken == TokenType.Minus)
            {
                var op = CurrentToken;
                GoToNextToken();
                var unaryExpression = new AstUnaryExpression()
                {
                    Operator = op,
                    Expression = (AstExpression)ParseFactor()
                };
                return unaryExpression;
            }
            var numberExpression = new AstNumberExpression() { Value = this.ts.Current.Value };
            GoToNextToken();
            return numberExpression;
        }
    }
}
