using System.Collections.Generic;

namespace Soltys.VirtualMachine.Test.TestUtils
{
    public class CallCountingRuntime : IRuntimeVisitorFactory, IRuntimeVisitor
    {
        private readonly Dictionary<string, int> callCount = new Dictionary<string, int>();
        public IReadOnlyDictionary<string, int> CallCount => this.callCount;

        public IRuntimeVisitor Create(IVMContext context) => this;

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

        public void VisitAdd(AddInstruction instruction) => IncrementCallCount(nameof(VisitAdd));
        public void VisitBranch(BranchInstruction instruction) => IncrementCallCount(nameof(VisitBranch));
        public void VisitCall(CallInstruction instruction) => IncrementCallCount(nameof(VisitCall));
        public void VisitCompare(CompareInstruction instruction) => IncrementCallCount(nameof(VisitCompare));
        public void VisitDivision(DivisionInstruction instruction) => IncrementCallCount(nameof(VisitDivision));
        public void VisitLoadConstant(LoadConstantInstruction instruction) => IncrementCallCount(nameof(VisitLoadConstant));
        public void VisitLoadPlace(LoadPlaceInstruction instruction) => IncrementCallCount(nameof(VisitLoadPlace));
        public void VisitLoadString(LoadStringInstruction instruction) => IncrementCallCount(nameof(VisitLoadString));
        public void VisitLoadLibrary(LoadLibraryInstruction instruction) => IncrementCallCount(nameof(VisitLoadLibrary));
        public void VisitMultiplication(MultiplicationInstruction instruction) => IncrementCallCount(nameof(VisitMultiplication));
        public void VisitNop(NopInstruction instruction) => IncrementCallCount(nameof(VisitNop));
        public void VisitList(ListInstruction listInstruction) => IncrementCallCount(nameof(VisitList));
        public void VisitReturn(ReturnInstruction instruction) => IncrementCallCount(nameof(VisitReturn));
        public void VisitStore(StoreInstruction instruction) => IncrementCallCount(nameof(VisitStore));
        public void VisitSubtraction(SubtractionInstruction instruction) => IncrementCallCount(nameof(VisitSubtraction));

    }
}
