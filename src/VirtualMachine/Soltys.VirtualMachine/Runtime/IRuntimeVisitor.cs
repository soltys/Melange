namespace Soltys.VirtualMachine
{
    public interface IRuntimeVisitor
    {
        //
        // Math
        //
        void VisitAdd(AddInstruction instruction);
        void VisitSubtraction(SubtractionInstruction instruction);
        void VisitMultiplication(MultiplicationInstruction instruction);
        void VisitDivision(DivisionInstruction instruction);

        //
        // Memory Management
        //
        void VisitLoadConstant(LoadConstantInstruction instruction);
        void VisitLoadPlace(LoadPlaceInstruction instruction);
        void VisitLoadString(LoadStringInstruction instruction);
        void VisitLoadLibrary(LoadLibraryInstruction instruction);
        void VisitStore(StoreInstruction instruction);

        //
        // Branching
        //
        void VisitBranch(BranchInstruction instruction);
        void VisitCall(CallInstruction instruction);
        
        
        
        void VisitReturn(ReturnInstruction instruction);
        void VisitCompare(CompareInstruction instruction);

        //Other
        void VisitNop(NopInstruction instruction);
        void VisitList(ListInstruction listInstruction);

    }
}
