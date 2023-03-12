using Soltys.Lisp.Compiler;

namespace Soltys.Lisp.Test.Compiler;

internal class AstVisitorCallCounting : IAstVisitor
{
    private readonly Dictionary<string, int> callCount = new Dictionary<string, int>();
    public IReadOnlyDictionary<string, int> CallCount => this.callCount;

    private void IncrementCallCount(string name)
    {
        if (!this.callCount.ContainsKey(name))
        {
            this.callCount.Add(name, 1);
        }
        else
        {
            this.callCount[name]++;
        }
    }

    public void VisitList(AstList ast) => IncrementCallCount(nameof(VisitList));
    public void VisitNumber(AstNumber ast) => IncrementCallCount(nameof(VisitNumber));
    public void VisitSymbol(AstSymbol ast) => IncrementCallCount(nameof(VisitSymbol));
    public void VisitString(AstString ast) => IncrementCallCount(nameof(VisitString));
}
