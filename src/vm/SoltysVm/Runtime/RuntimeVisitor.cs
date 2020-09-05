using System;
using System.Runtime.InteropServices.ComTypes;

namespace SoltysVm
{
    internal class RuntimeVisitor : IRuntimeVisitor
    {
        private IVMContext context;

        public RuntimeVisitor(IVMContext context)
        {
            this.context = context;
        }

        public void VisitAdd(AddInstruction instruction)
        {
            var rhs = this.context.ValueStack.Pop();
            var lhs = this.context.ValueStack.Pop();
            object result;
            if (rhs is int)
            {
                result = (int)lhs + (int)rhs;
            }
            else if (rhs is double)
            {
                result = (double)lhs + (double)rhs;
            }
            else
            {
                throw new InvalidOperationException();
            }

            this.context.ValueStack.Push(result);
        }

        public void VisitBranch(BranchInstruction instruction) => throw new NotImplementedException();

        public void VisitCall(CallInstruction instruction) => throw new NotImplementedException();

        public void VisitCompare(CompareInstruction instruction) => throw new NotImplementedException();

        public void VisitDivision(DivisionInstruction instruction)
        {
            var rhs = this.context.ValueStack.Pop();
            var lhs = this.context.ValueStack.Pop();

            object result;
            if (rhs is int)
            {
                result = (int)lhs / (int)rhs;
            }
            else if (rhs is double)
            {
                result = (double)lhs / (double)rhs;
            }
            else
            {
                throw new InvalidOperationException();
            }

            this.context.ValueStack.Push(result);
        }

        public void VisitLoadConstant(LoadConstantInstruction instruction)
        {
            this.context.ValueStack.Push(instruction.Value);
        }

        public void VisitLoadPlace(LoadPlaceInstruction instruction) => throw new NotImplementedException();

        public void VisitLoadString(LoadStringInstruction instruction) => throw new NotImplementedException();

        public void VisitMultiplication(MultiplicationInstruction instruction)
        {
            var rhs = this.context.ValueStack.Pop();
            var lhs = this.context.ValueStack.Pop();

            object result;
            if (rhs is int)
            {
                result = (int)lhs * (int)rhs;
            }
            else if (rhs is double)
            {
                result = (double)lhs * (double)rhs;
            }
            else
            {
                throw new InvalidOperationException();
            }

            this.context.ValueStack.Push(result);
        }

        public void VisitNop(NopInstruction instruction) => throw new NotImplementedException();

        public void VisitReturn(ReturnInstruction instruction) => throw new NotImplementedException();

        public void VisitStore(StoreInstruction instruction) => throw new NotImplementedException();

        public void VisitSubtraction(SubtractionInstruction instruction)
        {
            var rhs = this.context.ValueStack.Pop();
            var lhs = this.context.ValueStack.Pop();

            object result;
            if (rhs is int)
            {
                result = (int)lhs - (int)rhs;
            }
            else if (rhs is double)
            {
                result = (double)lhs - (double)rhs;
            }
            else
            {
                throw new InvalidOperationException();
            }

            this.context.ValueStack.Push(result);
        }
    }
}
