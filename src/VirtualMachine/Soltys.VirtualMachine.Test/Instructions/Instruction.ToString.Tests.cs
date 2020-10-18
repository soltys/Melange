using Xunit;

namespace Soltys.VirtualMachine.Test
{
    public class InstructionToStringTests
    {
        [Theory]
        [InlineData(LoadKind.Local, 1, "ldloc.1")]
        [InlineData(LoadKind.Argument, 0, "ldarg.0")]
        [InlineData(LoadKind.StaticField, 3, "ldsfld.3")]
        public void ToString_LoadPlaceInstruction(LoadKind loadKind, byte index, string expectedString)
        {
            var loadInstruction = new LoadPlaceInstruction(loadKind, index);
            Assert.Equal(expectedString, loadInstruction.ToString());
        }

        [Theory]
        [InlineData(StoreKind.Local, 1, "stloc.1")]
        [InlineData(StoreKind.Argument, 3, "starg.3")]
        [InlineData(StoreKind.Field, 2, "stfld.2")]
        [InlineData(StoreKind.StaticField, 2, "stsfld.2")]
        public void ToString_StoreInstruction(StoreKind storeKind, byte index, string expectedString)
        {
            var storeInstruction = new StoreInstruction(storeKind, index);
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
            var loadInstruction = new LoadConstantInstruction(LoadKind.Integer, 42);
            Assert.Equal("ldc.i 42", loadInstruction.ToString());
        }

        [Fact]
        public void ToString_LoadConstantInstruction_Double()
        {
            var loadInstruction = new LoadConstantInstruction(LoadKind.Double, 0.69);
            Assert.Equal("ldc.d 0.69", loadInstruction.ToString());
        }

        [Fact]
        public void ToString_LoadLibraryInstruction()
        {
            var loadInstruction = new LoadLibraryInstruction("MyLib");
            Assert.Equal("ldl MyLib", loadInstruction.ToString());
        }

        [Theory]
        [InlineData(CompareKind.Equals, "ceq")]
        [InlineData(CompareKind.GreaterThan, "cgt")]
        [InlineData(CompareKind.LessThan, "clt")]
        public void ToString_CompareInstruction(CompareKind compareKind, string expectedString)
        {
            var compareInstruction = new CompareInstruction(compareKind);
            Assert.Equal(expectedString, compareInstruction.ToString());
        }

        [Theory]
        [InlineData(BranchKind.Jump, "br 42")]
        [InlineData(BranchKind.IfFalse, "br.false 42")]
        [InlineData(BranchKind.IfTrue, "br.true 42")]
        public void ToString_BranchInstruction(BranchKind branchKind, string expectedString)
        {
            var branchInstruction = new BranchInstruction(branchKind, 42);
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
        public void ToString_ListNewInstruction() =>
            AssertToString<ListNewInstruction>("list.new");

        [Fact]
        public void ToString_ListAddInstruction() =>
            AssertToString<ListAddInstruction>("list.add");

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
