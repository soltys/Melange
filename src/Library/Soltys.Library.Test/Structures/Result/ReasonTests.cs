using Xunit;

namespace Soltys.Library.Test;

public class ReasonTests
{
    public static IEnumerable<object[]> ConstructorSetMessage = new[]{
        new object[]{new Reason(), string.Empty},
        new object[]{new Error("foo"), "foo"},
        new object[]{new Success("bar"), "bar"},
    };

    [Theory]
    [MemberData(nameof(ReasonTests.ConstructorSetMessage))]
    public void Constructor_SetsMessage(Reason reason, string expectedMessage) =>
        Assert.Equal(expectedMessage, reason.Message);
}
