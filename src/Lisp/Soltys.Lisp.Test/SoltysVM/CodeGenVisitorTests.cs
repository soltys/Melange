using Xunit;

namespace Soltys.Lisp.Test.SoltysVM;

public class CodeGenVisitorTests
{
    [Fact]
    public void Constructor_PassingNull_ThrowsArgumentNullException() => 
        Assert.Throws<ArgumentNullException>(() => new CodeGenVisitor(null));
}
