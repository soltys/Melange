using Xunit;

namespace Soltys.Library.Test;

public class BinaryStringNVarFieldTests
{
    private readonly BinaryStringNVarField sut;

    public BinaryStringNVarFieldTests()
    {
        this.sut = new BinaryStringNVarField(new byte[258], 0, 8);
    }

    [Fact]
    public void SetValue_TooLongString_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => this.sut.SetValue("123456789"));
    }

    [Fact]
    public void SetValue_NullString_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => this.sut.SetValue(null));
    }
}
