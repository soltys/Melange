using Xunit;

namespace Soltys.Database.Test.Cmd;

internal class TestParserVisitor : IAstVisitor
{
    private IAstNode expected;


    public void AssertVisit(IAstNode expectedVisit, IAstNode actualVisit)
    {
        this.expected = expectedVisit;
        Visit(actualVisit);
    }

    private void Visit(IAstNode node) => node.Accept(this);

    public void VisitExpression(AstExpression expression)
    {
        Assert.IsType<AstExpression>(this.expected);
        var expectedAst = (AstExpression)this.expected;

        Assert.Equal(expectedAst.Value, expression.Value);
    }

    public void VisitNumberExpression(AstNumberExpression number)
    {
        Assert.IsType<AstNumberExpression>(this.expected);
        var expectedAst = (AstNumberExpression)this.expected;
        Assert.Equal(expectedAst.Value, number.Value);
    }

    public void VisitBinaryExpression(AstBinaryExpression binaryExpression)
    {
        Assert.IsType<AstBinaryExpression>(this.expected);
        var expectedAst = (AstBinaryExpression)this.expected;
        Assert.Equal(expectedAst.Operator, binaryExpression.Operator);

        AssertVisit(expectedAst.Rhs, binaryExpression.Rhs);
        AssertVisit(expectedAst.Lhs, binaryExpression.Lhs);
    }

    public void VisitUnaryExpression(AstUnaryExpression unaryExpression)
    {
        Assert.IsType<AstUnaryExpression>(this.expected);
        var expectedAst = (AstUnaryExpression)this.expected;
        Assert.Equal(expectedAst.Operator, unaryExpression.Operator);

        AssertVisit(expectedAst.Expression, unaryExpression.Expression);
    }

    public void VisitFunctionCallExpression(AstFunctionCallExpression functionCallExpression)
    {
        Assert.IsType<AstFunctionCallExpression>(this.expected);
        var expectedAst = (AstFunctionCallExpression)this.expected;

        Assert.Equal(expectedAst.MethodCall, functionCallExpression.MethodCall);
        Assert.True(expectedAst.Arguments.Length == functionCallExpression.Arguments.Length, "Expected length of arguments is not equal to actual");

        for (int i = 0; i < functionCallExpression.Arguments.Length; i++)
        {
            AssertVisit(expectedAst.Arguments[i], functionCallExpression.Arguments[i]);
        }
    }

    public void VisitSelectStatement(AstSelectStatement selectStatement)
    {
        Assert.IsType<AstSelectStatement>(this.expected);
        var expectedAst = (AstSelectStatement)this.expected;
    }

    public void VisitInsertStatement(AstInsertStatement insertStatement)
    {
        Assert.IsType<AstInsertStatement>(this.expected);
        var expectedAst = (AstInsertStatement)this.expected;

        AssertVisit(expectedAst.Location, insertStatement.Location);
        AssertVisit(expectedAst.Values, insertStatement.Values);
    }

    public void VisitLocation(AstLocation location)
    {
        Assert.IsType<AstLocation>(this.expected);
        var expectedAst = (AstLocation)this.expected;

        Assert.Equal(expectedAst.Value, location.Value);
    }

    public void VisitValue(AstValue value)
    {
        Assert.IsType<AstValue>(this.expected);
        var expectedAst = (AstValue)this.expected;

        Assert.True(expectedAst.Expressions.Length == value.Expressions.Length, "Expected length of expressions is not equal to actual");
        for (int i = 0; i < value.Expressions.Length; i++)
        {
            AssertVisit(expectedAst.Expressions[i], value.Expressions[i]);
        }
    }
}