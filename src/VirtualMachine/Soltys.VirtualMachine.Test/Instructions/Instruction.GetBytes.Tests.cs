using System;
using Soltys.VirtualMachine.Test.TestUtils;
using Xunit;

namespace Soltys.VirtualMachine.Test
{
    public class InstructionGetBytesTests
    {
        [Theory]
        [InlineData(StoreKind.Local)]
        [InlineData(StoreKind.Argument)]
        [InlineData(StoreKind.StaticField)]
        public void GetBytes_StoreInstruction(StoreKind storeKind)
        {
            var instruction = new StoreInstruction(storeKind, (byte)0x03);
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Store, storeKind, (byte)0x03)
                .AsSpan();

            var actualBytes = instruction.GetBytes();
            Assert.True(expectedBytes.SequenceEqual(actualBytes));
        }

        [Theory]
        [InlineData(LoadKind.Local)]
        [InlineData(LoadKind.Argument)]
        [InlineData(LoadKind.StaticField)]
        public void GetBytes_LoadPlaceInstruction(LoadKind loadKind)
        {
            var instruction = new LoadPlaceInstruction(loadKind, (byte)0x03);
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, loadKind, (byte)0x03)
                .AsSpan();

            var actualBytes = instruction.GetBytes();
            Assert.True(expectedBytes.SequenceEqual(actualBytes));
        }

        [Theory]
        [InlineData(LoadKind.Integer, 42)]
        [InlineData(LoadKind.Double, 42.69)]
        public void GetBytes_LoadConstantInstruction(LoadKind loadKind, object value)
        {
            var instruction = new LoadConstantInstruction(loadKind, value);
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, loadKind, value)
                .AsSpan();

            Assert.True(expectedBytes.SequenceEqual(instruction.GetBytes()));
        }

        [Fact]
        public void GetBytes_LoadStringInstruction()
        {
            var instruction = new LoadStringInstruction("helloWorld");
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, LoadKind.String, "helloWorld")
                .AsSpan();

            Assert.True(expectedBytes.SequenceEqual(instruction.GetBytes()));
        }

        [Theory]
        [InlineData(CompareKind.Equals)]
        [InlineData(CompareKind.GreaterThan)]
        [InlineData(CompareKind.LessThan)]
        public void GetBytes_CompareInstruction(CompareKind compareKind)
        {
            var instruction = new CompareInstruction(compareKind);
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Compare, compareKind)
                .AsSpan();

            Assert.True(expectedBytes.SequenceEqual(instruction.GetBytes()));
        }


        [Theory]
        [InlineData(BranchKind.Jump)]
        [InlineData(BranchKind.IfFalse)]
        [InlineData(BranchKind.IfTrue)]
        public void GetBytes_BranchInstruction(BranchKind branchKind)
        {
            var instruction = new BranchInstruction(branchKind, 42);
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Branch, branchKind, 42)
                .AsSpan();

            Assert.True(expectedBytes.SequenceEqual(instruction.GetBytes()));
        }

        [Fact]
        public void GetBytes_CallInstruction()
        {
            const string methodCall = "void HelloWorld()";
            var instruction = new CallInstruction(methodCall);
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Call, methodCall)
                .AsSpan();

            Assert.True(expectedBytes.SequenceEqual(instruction.GetBytes()));
        }

        #region Opcodes without operands

        [Fact]
        public void GetBytes_AddInstruction() => AssertGetBytes<AddInstruction>(Opcode.Add);

        [Fact]
        public void GetBytes_SubtractionInstruction() => AssertGetBytes<SubtractionInstruction>(Opcode.Subtraction);

        [Fact]
        public void GetBytes_MultiplicationInstruction() => AssertGetBytes<MultiplicationInstruction>(Opcode.Multiplication);

        [Fact]
        public void GetBytes_DivisionInstruction() => AssertGetBytes<DivisionInstruction>(Opcode.Division);

        [Fact]
        public void GetBytes_NopInstruction() => AssertGetBytes<NopInstruction>(Opcode.Nop);

        [Fact]
        public void GetBytes_ReturnInstruction() => AssertGetBytes<ReturnInstruction>(Opcode.Return);

        private static void AssertGetBytes<TInstruction>(Opcode opcode) where TInstruction : IInstruction, new()
        {
            var instruction = new TInstruction();
            var expectedBytes = InstructionByteBuilder.Create().Opcode(opcode).AsSpan();
            Assert.True(expectedBytes.SequenceEqual(instruction.GetBytes()));
        }

        #endregion
    }
}
