using Xunit;

namespace Soltys.Lisp.Test
{
    public partial class SoltysLispTests
    {
        [Fact]
        public void Do_DefineFunctionWithConstantValue_ReturnConstantValue()
        {
            using var lisp = new SoltysLisp();
            lisp.Initialize();
            var result = lisp.Do(
                @"(def! x 42)
                  (+ 8 x)");
            Assert.IsType<int>(result);
            var intValue = (int)result;
            Assert.Equal(50, intValue);
        }

        [Fact]
        public void TwoDo_DefineFunctionWithConstantValue_ReturnConstantValue()
        {
            using var lisp = new SoltysLisp();
            lisp.Initialize();
            var result = lisp.Do(@"(def! x 42)");
            result = lisp.Do(@"(+ 8 x)");
            Assert.IsType<int>(result);
            var intValue = (int)result;
            Assert.Equal(50, intValue);
        }

        [Fact]
        public void Do_DefineFunctionWithMathOperation_ReturnMathResult()
        {
            using var lisp = new SoltysLisp();
            lisp.Initialize();
            var result = lisp.Do(
                @"(def! x (* 4 10))
                  (+ x 8)");
            Assert.IsType<int>(result);
            var intValue = (int)result;
            Assert.Equal(48, intValue);
        }

        [Fact]
        public void Do_MultipleFunctionsCall()
        {
            using var lisp = new SoltysLisp();
            lisp.Initialize();
            var result = lisp.Do(
                @"(def! x (* 4 10))
                  (+ x (/ x x))");
            Assert.IsType<int>(result);
            var intValue = (int)result;
            Assert.Equal(41, intValue);
        }

        [Fact]
        public void Do_RedefineMethod()
        {
            using var lisp = new SoltysLisp();
            lisp.Initialize();
            var result = lisp.Do(
                @"(def! x (* 5 5))
                  (def! x (* 6 6))
                  (x) ");
            Assert.IsType<int>(result);
            var intValue = (int)result;
            Assert.Equal(36, intValue);
        }

        [Fact]
        public void TwoDo_RedefineMethod()
        {
            using var lisp = new SoltysLisp();
            lisp.Initialize();
            var result = lisp.Do(
                @"(def! x (* 5 5))
                  (def! y (* 10 x))");
            result = lisp.Do(
                @"(def! x (* 6 6))
                  (y)");
            Assert.IsType<int>(result);
            var intValue = (int)result;
            Assert.Equal(360, intValue);
        }
    }
}
