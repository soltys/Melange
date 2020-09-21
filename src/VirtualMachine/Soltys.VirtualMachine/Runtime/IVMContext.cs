using System.Collections.Generic;

namespace Soltys.VirtualMachine
{
    public interface IVMContext
    {
        Stack<object> ValueStack
        {
            get;
        }
    }
}
