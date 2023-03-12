using Soltys.Library.TextAnalysis;
using Xunit;

namespace Soltys.Library.Test.TextAnalysis;

public class PositionTests
{
    [Fact]
    public void Constructor_Postion_SetsLineAndColumn()
    {
        var position = new Position(42,69);
        Assert.Equal(42, position.Line);
        Assert.Equal(69, position.Column);
    }

    [Fact]
    public void Empty_Position_EqualToZero()
    {
        Assert.Equal(new Position(0, 0), Position.Empty);
        Assert.Equal(default, Position.Empty);
    }
}