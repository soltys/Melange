using Xunit;

namespace Soltys.Lisp.Test
{
    public partial class SoltysLispTests
    {
        [Fact]
        public void Do_DoNotHoldLastValueBetweenCalls()
        {
            using var lisp = new SoltysLisp();

            var result = lisp.Do(@"(+ 10 1)");
            Assert.Equal(11, (int)result);
            var newResult = lisp.Do(@"()");
            Assert.Equal("nil", (string)newResult);
        }
        

        [Fact]
        public void Do_Println_ResultIsNil()
        {
            using var lisp = new SoltysLisp();
            lisp.Initialize();

            var result = lisp.Do(@"(println ""Hello, World!"")");

            Assert.Equal("nil", result);
        }
    }
}
