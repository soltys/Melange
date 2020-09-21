using Xunit;

namespace Soltys.VirtualMachine.Test
{
    public class InstructionToStringTests
    {
        [Theory]
        [InlineData(LoadType.Local, 1, "ldloc.1")]
        [InlineData(LoadType.Argument, 0, "ldarg.0")]
        [InlineData(LoadType.StaticField, 3, "ldsfld.3")]
        public void ToString_LoadPlaceInstruction(LoadType loadType, byte index, string expectedString)
        {
            var loadInstruction = new LoadPlaceInstruction(loadType, index);
            Assert.Equal(expectedString, loadInstruction.ToString());
        }

        [Theory]
        [InlineData(StoreType.Local, 1, "stloc.1")]
        [InlineData(StoreType.Argument, 3, "starg.3")]
        [InlineData(StoreType.Field, 2, "stfld.2")]
        [InlineData(StoreType.StaticField, 2, "stsfld.2")]
        public void ToString_StoreInstruction(StoreType storeType, byte index, string expectedString)
        {
            var storeInstruction = new StoreInstruction(storeType, index);
            Assert.Equal(expectedString, storeInstruction.ToString());
        }

        [Fact]
        public void ToString_LoadStringInstruction()
        {
            var loadInstruction = new LoadStringInstruction("helloWorld");
            Assert.Equal("ldstr helloWorld", loadInstruction.ToString());
        }

        [Fact]
        public void ToString_LoadConstantInstruction_Integer()
        {
            var loadInstruction = new LoadConstantInstruction(LoadType.Integer, 42);
            Assert.Equal("ldc.i 42", loadInstruction.ToString());
        }

        [Fact]
        public void ToString_LoadConstantInstruction_Double()
        {
            var loadInstruction = new LoadConstantInstruction(LoadType.Double, 0.69);
            Assert.Equal("ldc.d 0.69", loadInstruction.ToString());
        }

        [Theory]
        [InlineData(CompareType.Equals, "ceq")]
        [InlineData(CompareType.GreaterThan, "cgt")]
        [InlineData(CompareType.LessThan, "clt")]
        public void ToString_CompareInstruction(CompareType compareType, string expectedString)
        {
            var compareInstruction = new CompareInstruction(compareType);
            Assert.Equal(expectedString, compareInstruction.ToString());
        }

        [Theory]
        [InlineData(BranchType.Jump, "br 42")]
        [InlineData(BranchType.IfFalse, "br.false 42")]
        [InlineData(BranchType.IfTrue, "br.true 42")]
        public void ToString_BranchInstruction(BranchType branchType, string expectedString)
        {
            var branchInstruction = new BranchInstruction(branchType, 42);
            Assert.Equal(expectedString, branchInstruction.ToString());
        }

        [Fact]
        public void ToString_CallInstruction()
        {
            var callInstruction = new CallInstruction("void HelloWorld()");
            Assert.Equal("call void HelloWorld()", callInstruction.ToString());
        }

        #region Opcode without operands

        [Fact]
        public void ToString_NopInstruction() =>
            AssertToString<NopInstruction>("nop");

        [Fact]
        public void ToString_ReturnInstruction() =>
            AssertToString<ReturnInstruction>("ret");

        [Fact]
        public void ToString_AddInstruction() =>
            AssertToString<AddInstruction>("add");

        [Fact]
        public void ToString_SubtractionInstruction() =>
            AssertToString<SubtractionInstruction>("sub");

        [Fact]
        public void ToString_MultiplicationInstruction() =>
            AssertToString<MultiplicationInstruction>("mul");

        [Fact]
        public void ToString_DivisionInstruction() =>
            AssertToString<DivisionInstruction>("div");

        private static void AssertToString<T>(string expectedToString) where T : new()
        {
            var instruction = new T();
            Assert.Equal(expectedToString, instruction.ToString());
        }
        #endregion

    }
}
