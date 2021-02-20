using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soltys.Library.TextAnalysis;
using Soltys.Library.VisualStudioSolution;
using Xunit;

namespace Soltys.Library.Test
{
    public class SolutionTokenTests
    {
        [Fact]
        public void Empty_IsCorrectlyDefined()
        {
            Assert.Equal(SolutionTokenKind.Undefined, SolutionToken.Empty.TokenKind);
            Assert.Equal(string.Empty, SolutionToken.Empty.Value);
            Assert.Equal(Position.Empty, SolutionToken.Empty.Position);
        }
    }
}
