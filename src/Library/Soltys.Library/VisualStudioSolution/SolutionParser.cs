using Soltys.Library.TextAnalysis;

namespace Soltys.Library.VisualStudioSolution
{
    public class SolutionParser : ParserBase<SolutionToken, SolutionTokenKind>
    {
        public SolutionParser(ITokenSource<SolutionToken, SolutionTokenKind> ts) : base(ts)
        {
        }

        public void ParseSolution()
        {
            ParseHeader();
            ParseBody();
        }

        private void ParseHeader()
        {
        }

        private void ParseBody()
        {
        }

      
    }
}
