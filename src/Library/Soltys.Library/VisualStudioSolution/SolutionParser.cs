using System;
using System.Collections.Generic;
using Soltys.Library.TextAnalysis;

namespace Soltys.Library.VisualStudioSolution
{
    public class SolutionParser : ParserBase<SolutionToken, SolutionTokenKind>
    {
        public SolutionParser(ITokenSource<SolutionToken, SolutionTokenKind> ts) : base(ts)
        {
        }

        public AstSolution ParseSolution() =>
            new AstSolution(ParseHeader(), ParseBody());

        private AstHeader ParseHeader()
        {
            var fileVersion = GetNextVersionTokenValue();
            var iconVersion = GetNextVersionTokenValue();
            var lastAccessVersion = GetNextVersionTokenValue();
            var minimumVisualStudioVersion = GetNextVersionTokenValue();
            AdvanceToken(SolutionTokenKind.NewLine);
            return new AstHeader(fileVersion, iconVersion, lastAccessVersion, minimumVisualStudioVersion);
        }

        private string GetNextVersionTokenValue()
        {
            while (!IsToken(SolutionTokenKind.Version))
            {
                this.ts.NextToken();
            }

            string versionToken = this.ts.Current.Value;
            this.ts.NextToken();
            return versionToken;
        }

        private AstBody ParseBody() =>
            new AstBody(ParseProjects(), ParseGlobal());

        private IEnumerable<AstProject> ParseProjects()
        {
            List<AstProject> projects = new List<AstProject>();

            while (IsToken(SolutionTokenKind.Id) && this.ts.Current.Value == "Project")
            {
                AdvanceTokenValueMatch(SolutionTokenKind.Id, "Project");
                AdvanceToken(SolutionTokenKind.LParen);
                var projectKind = this.ts.Current.Value;
                AdvanceToken(SolutionTokenKind.String);
                AdvanceToken(SolutionTokenKind.RParen);
                AdvanceToken(SolutionTokenKind.Equal);
                var name = this.ts.Current.Value;
                AdvanceToken(SolutionTokenKind.String);
                AdvanceToken(SolutionTokenKind.Comma);
                var path = this.ts.Current.Value;
                AdvanceToken(SolutionTokenKind.String);
                AdvanceToken(SolutionTokenKind.Comma);
                var guid = this.ts.Current.Value;
                AdvanceToken(SolutionTokenKind.String);
                AdvanceToken(SolutionTokenKind.NewLine);
                AdvanceTokenValueMatch(SolutionTokenKind.Id, "EndProject");
                AdvanceToken(SolutionTokenKind.NewLine);
                projects.Add(new AstProject(projectKind, name, path, guid));
            }
            return projects;
        }

        private AstGlobal ParseGlobal()
        {
            AdvanceTokenValueMatch(SolutionTokenKind.Id, "Global");
            AdvanceToken(SolutionTokenKind.NewLine);
            var globalSections = ParseGlobalSections();
            AdvanceTokenValueMatch(SolutionTokenKind.Id, "EndGlobal");
            AdvanceToken(SolutionTokenKind.NewLine);
            return new AstGlobal(globalSections);
        }

        private IEnumerable<AstGlobalSection> ParseGlobalSections()
        {
            var sections = new List<AstGlobalSection>();
            while (IsToken(SolutionTokenKind.Id) && this.ts.Current.Value == "GlobalSection")
            {
                AdvanceTokenValueMatch(SolutionTokenKind.Id, "GlobalSection");
                AdvanceToken(SolutionTokenKind.LParen);
                var sectionKind = this.ts.Current.Value;
                AdvanceToken(SolutionTokenKind.Id);
                AdvanceToken(SolutionTokenKind.RParen);
                AdvanceToken(SolutionTokenKind.Equal);
                var action = this.ts.Current.Value;
                AdvanceToken(SolutionTokenKind.Id);
                AdvanceToken(SolutionTokenKind.NewLine);
                sections.Add(new AstGlobalSection(sectionKind, action, ParseSectionEntries()));
            }

            return sections;
        }

        private IEnumerable<SectionEntry> ParseSectionEntries()
        {
            var entries = new List<SectionEntry>();

            while (!IsToken(SolutionTokenKind.Id) || this.ts.Current.Value != "EndGlobalSection")
            {
                var tokens = new List<SolutionToken>();
                while (!IsToken(SolutionTokenKind.NewLine))
                {
                    tokens.Add(this.ts.Current);
                    this.ts.NextToken();
                }
                entries.Add(new SectionEntry(tokens));
                AdvanceToken(SolutionTokenKind.NewLine);
            }
            AdvanceTokenValueMatch(SolutionTokenKind.Id, "EndGlobalSection");
            AdvanceToken(SolutionTokenKind.NewLine);
            return entries;
        }
    }
}
