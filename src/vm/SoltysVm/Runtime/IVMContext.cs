using System.Collections.Generic;

namespace SoltysVm
{
    public interface IVMContext
    {
        Stack<object> ValueStack
        {
            get;
        }
    }
}