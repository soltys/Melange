using System.Collections.Generic;
using Soltys.VirtualMachine.Contracts;

namespace Soltys.VirtualMachine
{
    public interface IVMContext
    {
        Stack<object> ValueStack
        {
            get;
        }

        void AddVMLibrary(IVMLibrary library);
        IVMExternalFunction FindExternalFunction(string methodName);
        bool TryChangeFunction(string methodName);
        void ReturnFromMethod();
    }
}
