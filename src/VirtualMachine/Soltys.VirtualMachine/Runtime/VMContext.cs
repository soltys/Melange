using System;
using System.Collections.Generic;
using System.Linq;
using Soltys.VirtualMachine.Contracts;

namespace Soltys.VirtualMachine
{
    internal class VMContext : IVMContext
    {
        private readonly HashSet<VMFunction> functions;
        private readonly List<IVMLibrary> vmLibraries;
        private VMFunction? currentFunction;

        public Stack<object> ValueStack
        {
            get;
        }

        private readonly Stack<CallEntry> callstack;

        public VMContext()
        {
            this.functions = new HashSet<VMFunction>();
            this.vmLibraries = new List<IVMLibrary>();
            ValueStack = new Stack<object>();
            this.callstack = new Stack<CallEntry>();
        }

        public void Load(IEnumerable<VMFunction> vmFunctions)
        {
            this.functions.RemoveWhere(x => vmFunctions.Any(y => y.Equals(x)));
            this.functions.UnionWith(vmFunctions);
        }

        private int IP => this.callstack.Peek().InstructionPointer;
        public IInstruction? GetCurrentInstruction() => this.currentFunction?.Instructions?[IP];
        public bool IsHalted() => IP < this.currentFunction?.Instructions?.Length;

        public void AdvanceInstructionPointer() =>
            this.callstack.Peek().AdvanceInstructionPointer();

        public void AddVMLibrary(IVMLibrary library) => this.vmLibraries.Add(library);
        public IVMExternalFunction FindExternalFunction(string methodName)
        {
            foreach (var vmLibrary in this.vmLibraries.Where(vmLibrary => vmLibrary.Functions.ContainsKey(methodName)))
            {
                return vmLibrary.Functions[methodName];
            }

            throw new InvalidOperationException($"function named {methodName} was not found");
        }

        public bool TryChangeFunction(string methodName)
        {
            var fun = this.functions.FirstOrDefault(x => x.Name == methodName);
            if (fun == null)
            {
                return false;
            }
            // it set to -1 because it after completing 'call' instruction pointer will be increased to 0 - first
            // instruction of changed method. 
            ChangeMethod(new CallEntry(methodName, -1));
            return true;
        }

        public void ReturnFromMethod()
        {
            this.callstack.Pop();
            this.currentFunction = this.functions.FirstOrDefault(x => x.Name == this.callstack.Peek().MethodName);
        }

        public void ChangeMethod(CallEntry callEntry)
        {
            this.callstack.Push(callEntry);
            this.currentFunction = this.functions.First(x => x.Name == callEntry.MethodName);
        }

        public void Clear()
        {
            this.callstack.Clear();
            ValueStack.Clear();
            this.functions.Remove(new VMFunction("Main"));
        }
    }
}
