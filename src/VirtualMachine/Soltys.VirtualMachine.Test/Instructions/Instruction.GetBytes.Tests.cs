using System;
using Soltys.VirtualMachine.Test.TestUtils;
using Xunit;

namespace Soltys.VirtualMachine.Test
{
    public class InstructionGetBytesTests
    {
        [Theory]
        [InlineData(StoreType.Local)]
        [InlineData(StoreType.Argument)]
        [InlineData(StoreType.StaticField)]
        public void GetBytes_StoreInstruction(StoreType storeType)
        {
            var instruction = new StoreInstruction(storeType, (byte)0x03);
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Store, storeType, (byte)0x03)
                .AsSpan();

            var actualBytes = instruction.GetBytes();
            Assert.True(expectedBytes.SequenceEqual(actualBytes));
        }

        [Theory]
        [InlineData(LoadType.Local)]
        [InlineData(LoadType.Argument)]
        [InlineData(LoadType.StaticField)]
        public void GetBytes_LoadPlaceInstruction(LoadType loadType)
        {
            var instruction = new LoadPlaceInstruction(loadType, (byte)0x03);
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, loadType, (byte)0x03)
                .AsSpan();

            var actualBytes = instruction.GetBytes();
            Assert.True(expectedBytes.SequenceEqual(actualBytes));
        }

        [Theory]
        [InlineData(LoadType.Integer, 42)]
        [InlineData(LoadType.Double, 42.69)]
        public void GetBytes_LoadConstantInstruction(LoadType loadType, object value)
        {
            var instruction = new LoadConstantInstruction(loadType, value);
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, loadType, value)
                .AsSpan();

            Assert.True(expectedBytes.SequenceEqual(instruction.GetBytes()));
        }

        [Fact]
        public void GetBytes_LoadStringInstruction()
        {
            var instruction = new LoadStringInstruction("helloWorld");
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, LoadType.String, "helloWorld")
                .AsSpan();

            Assert.True(expectedBytes.SequenceEqual(instruction.GetBytes()));
        }

        [Theory]
        [InlineData(CompareType.Equals)]
        [InlineData(CompareType.GreaterThan)]
        [InlineData(CompareType.LessThan)]
        public void GetBytes_CompareInstruction(CompareType compareType)
        {
            var instruction = new CompareInstruction(compareType);
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Compare, compareType)
                .AsSpan();

            Assert.True(expectedBytes.SequenceEqual(instruction.GetBytes()));
        }


        [Theory]
        [InlineData(BranchType.Jump)]
        [InlineData(BranchType.IfFalse)]
        [InlineData(BranchType.IfTrue)]
        public void GetBytes_BranchInstruction(BranchType branchType)
        {
            var instruction = new BranchInstruction(branchType, 42);
            var expectedBytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Branch, branchType, 42)
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
