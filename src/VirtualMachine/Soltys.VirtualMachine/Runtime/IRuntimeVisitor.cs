namespace Soltys.VirtualMachine
{
    public interface IRuntimeVisitor
    {
        void VisitAdd(AddInstruction instruction);
        void VisitBranch(BranchInstruction instruction);
        void VisitCall(CallInstruction instruction);
        void VisitCompare(CompareInstruction instruction);
        void VisitDivision(DivisionInstruction instruction);
        void VisitLoadConstant(LoadConstantInstruction instruction);
        void VisitLoadPlace(LoadPlaceInstruction instruction);
        void VisitLoadString(LoadStringInstruction instruction);
        void VisitLoadLibrary(LoadLibraryInstruction instruction);
        void VisitMultiplication(MultiplicationInstruction instruction);
        void VisitNop(NopInstruction instruction);
        void VisitReturn(ReturnInstruction instruction);
        void VisitStore(StoreInstruction instruction);
        void VisitSubtraction(SubtractionInstruction instruction);
    }
}
