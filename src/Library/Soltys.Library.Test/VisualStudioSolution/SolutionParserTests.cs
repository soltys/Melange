using System.Linq;
using Soltys.Library.Test.VisualStudioSolution.TestUtils;
using Soltys.Library.TextAnalysis;
using Soltys.Library.VisualStudioSolution;
using Xunit;

namespace Soltys.Library.Test.VisualStudioSolution
{
    public class SolutionParserTests
    {
        [Fact]
        public void ParseSolution_GetsHeader()
        {
            var ast = ParserFactory(ExampleSolutionProvider.MsdnSample).ParseSolution();
            Assert.Equal("12.00", ast.Header.FileFormat);
            Assert.Equal("16", ast.Header.IconVersion);
            Assert.Equal("16.0.28701.123", ast.Header.LatestVisualStudioAccess);
            Assert.Equal("10.0.40219.1", ast.Header.MinimumVisualStudioVersion);
        }

        [Fact]
        public void ParseSolution_GetProjectInformation()
        {
            var ast = ParserFactory(ExampleSolutionProvider.MsdnSample).ParseSolution();
            var project = ast.Body.Projects.Single();

            Assert.Equal("{F184B08F-C81C-45F6-A57F-5ABD9991F28F}", project.Kind);
            Assert.Equal("Project1", project.Name);
            Assert.Equal("Project1.vbproj", project.Path);
            Assert.Equal("{8CDD8387-B905-44A8-B5D5-07BB50E05BEA}", project.Guid);
        }

        private static SolutionParser ParserFactory(string input) =>
            new SolutionParser(
                new TokenSource<SolutionToken, SolutionTokenKind>(
                    new SolutionLexer(
                        new TextSource(input))));
    }
}
