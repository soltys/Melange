using System;
using System.Collections.Generic;
using Soltys.VirtualMachine.Contracts;

namespace Soltys.Lisp.CoreLib
{
    public class VMLibrary : IVMLibrary
    {
        private Dictionary<string, IVMExternalFunction> functions;

        public VMLibrary()
        {
            this.functions = new Dictionary<string, IVMExternalFunction>();

            this.functions.Add("println", new VMExternalFunction(new Action<string>(Console.WriteLine)));
            this.functions.Add("strcat", new VMExternalFunction(
                new Func<string, string, string>(string.Concat)));
            this.functions.Add("str", new VMExternalFunction
                (new Func<object, string>((o) => o.ToString())));
        }

        public IReadOnlyDictionary<string, IVMExternalFunction> Functions => this.functions;
    }
}
