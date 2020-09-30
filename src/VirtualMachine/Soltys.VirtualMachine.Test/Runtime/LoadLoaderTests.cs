using Xunit;

namespace Soltys.VirtualMachine.Test.Runtime
{
    public class LoadLoaderTests
    {
        [Fact]
        public void LoadLibrary_LoadItself()
        {
            var library = LibraryLoader.LoadLibrary("Soltys.VirtualMachine.Test");
            Assert.NotNull(library.Functions);
        }

        [Fact]
        public void LoadLibrary_UseAddFunction()
        {
            var library = LibraryLoader.LoadLibrary("Soltys.VirtualMachine.Test");

            Assert.Contains("add", library.Functions);

            var result = library.Functions["add"].Execute(6, 7);
            Assert.Equal(13, (int)result);
        }
    }
}
