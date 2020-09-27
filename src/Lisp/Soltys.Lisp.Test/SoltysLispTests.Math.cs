using Xunit;

namespace Soltys.Lisp.Test
{
    public partial class  SoltysLispTests
    {
        [Theory]
        [InlineData("(+ 1 2)", 3)]
        [InlineData("(+ 5 (* 2 3))", 11)]
        [InlineData("(- (+ 5 (* 2 3)) 3)", 8)]
        [InlineData("(/ (- (+ 5 (* 2 3)) 3) 4)", 2)]
        [InlineData("(/ (- (+ 515 (* 87 311)) 302) 27)", 1010)]
        [InlineData("(* -3 6)", -18)]
        [InlineData("(/ (- (+ 515 (* -87 311)) 296) 27)", -994)]
        public void Do_StringInput_ExpectIntResult(string input, int expectedResult)
        {
            using var lisp = new SoltysLisp();
            var result = lisp.Do(input);

            Assert.IsType<int>(result);
            var intValue = (int)result;
            Assert.Equal(expectedResult, intValue);
        }

        [Theory]
        [InlineData("(+ 0.1 0.2)", 0.3)]
        [InlineData("(- 0.3 0.1)", 0.2)]
        [InlineData("(* 2.1 2.1)", 4.41)]
        [InlineData("(/ 2.1 0.1)", 21.0)]
        public void Do_StringInput_ExpectDoubleResult(string input, double expectedResult)
        {
            using var lisp = new SoltysLisp();
            var result = lisp.Do(input);

            Assert.IsType<double>(result);
            var doubleValue = (double)result;
            Assert.Equal(expectedResult, doubleValue, 5);
        }

    }
}
