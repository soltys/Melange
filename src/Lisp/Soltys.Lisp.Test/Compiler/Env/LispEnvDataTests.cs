using System.Linq;
using Soltys.Lisp.Compiler;
using Xunit;

namespace Soltys.Lisp.Test.Compiler
{
    public class LispEnvDataTests
    {
        [Fact]
        public void Clone_Defines_HasNotSameObject_ButEqual()
        {
            var data = new LispEnvData();
            var sourceKey = new AstSymbol("foo");
            var sourceValue = new AstSymbol("bar");
            data.Defines[sourceKey] = sourceValue;

            var dataClone = data.Clone();
            var keyClone = dataClone.Defines.Keys.Single();
            var valueClone = (AstSymbol)dataClone.Defines[sourceKey];

            Assert.NotSame(sourceKey, keyClone);
            Assert.NotSame(sourceValue, valueClone);

            Assert.Equal(sourceKey.Name, keyClone.Name);
            Assert.Equal(sourceValue.Name, valueClone.Name);
        }

        [Fact]
        public void RegisterLibraryNames_GivenListOnStrings_AccesableByClonedData()
        {
            var data = new LispEnvData();
            data.RegisterLibraryNames(new[] { "a", "b", "c" });
            IReadOnlyEnvData readOnlyData = data;
            Assert.Contains("a", readOnlyData.CoreFunctionNames);
            Assert.Contains("b", readOnlyData.CoreFunctionNames);
            Assert.Contains("c", readOnlyData.CoreFunctionNames);



        }
    }
}
