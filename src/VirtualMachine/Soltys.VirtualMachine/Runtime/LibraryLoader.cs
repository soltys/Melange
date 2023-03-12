using System.Reflection;
using Soltys.VirtualMachine.Contracts;

namespace Soltys.VirtualMachine;

internal class LibraryLoader
{
    public static IVMLibrary LoadLibrary(string name)
    {
        var assembly = Assembly.Load(new AssemblyName(name));
        var vmLibraryType = assembly.GetTypes().Single(t => t.IsPublic && typeof(IVMLibrary).IsAssignableFrom(t));
        var vmLibrary = (IVMLibrary)Activator.CreateInstance(vmLibraryType);
        return vmLibrary;
    }
}
