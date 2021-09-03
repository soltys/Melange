using System.Collections.Generic;
using Xunit;

namespace Soltys.Lisp.Test
{
    public partial class SoltysLispVMTests
    {
        [Fact]
        public void Do_DoNotHoldLastValueBetweenCalls()
        {
            using var lisp = new SoltysLispVM();

            var result = lisp.Do(@"(+ 10 1)");
            Assert.Equal(11, (int)result);
            var newResult = lisp.Do(@"()");
            Assert.Equal("nil", (string)newResult);
        }

        [Fact]
        public void Do_Println_ResultIsNil()
        {
            using var lisp = new SoltysLispVM();
            lisp.Initialize();

            var result = lisp.Do(@"(println ""Hello, World!"")");

            Assert.Equal("nil", result);
        }

        [Theory]
        [InlineData("'(1 2 3)")]
        [InlineData("(quote (1 2 3))")]
        public void Do_QuoteFunction_ResultIsListItself(string input)
        {
            using var lisp = new SoltysLispVM();
            lisp.Initialize();

            var result = lisp.Do(input);

            Assert.IsType<List<object>>(result);
            var theList = (List<object>)result;
            Assert.Equal(3, theList.Count);
            Assert.Equal(new object[] { 1, 2, 3 }, theList);
        }
    }
}
