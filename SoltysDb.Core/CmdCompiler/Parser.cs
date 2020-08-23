using System.Linq;

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
        private TokenType NextToken => this.ts.PeekNextToken.TokenType;
        private void GoToNextToken() => this.ts.NextToken();
        private bool IsToken(params TokenType[] tokens) => tokens.Any(x => x == CurrentToken);

        public AstNode ParseExpression()
        {
            var term = ParseTerm();

            if (IsToken(TokenType.Plus, TokenType.Minus))
            {
                var leftExpression = (AstExpression)term;
                var op = CurrentToken;
                GoToNextToken();
                var rightExpression = (AstExpression)ParseExpression();

                var binaryExpression = new AstBinaryExpression()
                {
                    Lhs = leftExpression,
                    Rhs = rightExpression,
                    Operator = op
                };
                return binaryExpression;
            }

            return term;

        }

        public AstNode ParseTerm()
        {
            var factor = ParseFactor();
            if (IsToken(TokenType.Star, TokenType.Slash))
            {
                var leftExpression = (AstExpression)factor;
                var op = CurrentToken;
                GoToNextToken();
                var rightExpression = (AstExpression)ParseTerm();

                var binaryExpression = new AstBinaryExpression()
                {
                    Lhs = leftExpression,
                    Rhs = rightExpression,
                    Operator = op
                };
                return binaryExpression;
            }
            return factor;
        }

        public AstNode ParseFactor()
        {
            // '(' expression ')'
            if (IsToken(TokenType.LParen))
            {
                GoToNextToken();
                var expression = ParseExpression();
                GoToNextToken();
                return expression;
            }

            if (IsToken(TokenType.Minus, TokenType.Plus))
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
