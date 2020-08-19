namespace SoltysDb.Core
{
    internal class Parser
    {
        private readonly ITokenSource ts;

        public Parser(ITokenSource ts)
        {
            this.ts = ts;
        }


        //expression = term {("+"|"-") term} 

        //term = factor {("*"|"/") factor}

        //factor =
        //ident
        //| number
        //| "(" expression ")"

        public AstNode ParseExpression()
        {
            var term = ParseTerm();

            if (this.ts.Current.TokenType == TokenType.Plus)
            {
                var leftExpression = (AstExpression)term;
                this.ts.NextToken();
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
            if (this.ts.Current.TokenType == TokenType.Star)
            {
                var leftExpression = (AstExpression)factor;
                this.ts.NextToken();
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
            if (this.ts.Current.TokenType == TokenType.LParen)
            {
                this.ts.NextToken();
                var expression = ParseExpression();
                this.ts.NextToken();
                return expression;
            }

            var numberExpression = new AstNumberExpression() { Value = this.ts.Current.Value };
            this.ts.NextToken();
            return numberExpression;
        }
    }
}
