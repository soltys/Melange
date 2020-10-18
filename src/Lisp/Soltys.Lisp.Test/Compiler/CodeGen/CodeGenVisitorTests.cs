using System;
using Soltys.Lisp.Compiler;
using Xunit;

namespace Soltys.Lisp.Test.Compiler
{
    public class CodeGenVisitorTests
    {
        [Fact]
        public void Constructor_PassingNull_ThrowsArgumentNullException() => 
            Assert.Throws<ArgumentNullException>(() => new CodeGenVisitor(null));
    }
}
