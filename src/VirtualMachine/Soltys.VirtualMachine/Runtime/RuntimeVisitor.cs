using System;
using System.Collections.Generic;
using Soltys.VirtualMachine.Contracts;

namespace Soltys.VirtualMachine
{
    internal class RuntimeVisitor : IRuntimeVisitor
    {
        private readonly IVMContext context;

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

        public void VisitCall(CallInstruction instruction)
        {
            var functionFound = this.context.TryChangeFunction(instruction.MethodName);
            if (functionFound)
            {
                return;
            }

            var vmFunc = this.context.FindExternalFunction(instruction.MethodName);
            CallExternalFunction(vmFunc);

            void CallExternalFunction(IVMExternalFunction vmFunc)
            {
                var parameters = new List<object>();

                for (int i = 0; i < vmFunc.ArgumentCount; i++)
                {
                    parameters.Add(this.context.ValueStack.Pop());
                }

                parameters.Reverse();

                var result = vmFunc.Execute(parameters.ToArray());

                if (result != null)
                {
                    this.context.ValueStack.Push(result);
                }
            }
        }

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

        public void VisitLoadString(LoadStringInstruction instruction)
        {
            this.context.ValueStack.Push(instruction.Value);
        }
        public void VisitLoadLibrary(LoadLibraryInstruction instruction)
        {
            var vmLibrary = LibraryLoader.LoadLibrary(instruction.LibraryName);
            this.context.AddVMLibrary(vmLibrary);
        }

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

        public void VisitReturn(ReturnInstruction instruction)
        {
            this.context.Return();
        }

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
