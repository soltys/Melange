using System;

namespace Soltys.Database
{
    internal class AstFunctionCallExpression : AstExpression, IAstNode
    {
        public AstFunctionCallExpression(string methodCall)
        {
            MethodCall = methodCall;
            Arguments = Array.Empty<AstExpression>();
        }

        public AstFunctionCallExpression(string methodCall, AstExpression[] expressions) : this(methodCall)
        {
            Arguments = expressions;
        }

        public void Accept(IAstVisitor visitor) => visitor.VisitFunctionCallExpression(this);
        public string MethodCall
        {
            get;
        }

        public AstExpression[] Arguments
        {
            get;
        }
    }
}
