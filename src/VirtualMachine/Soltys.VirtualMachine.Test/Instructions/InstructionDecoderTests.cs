using System;
using Soltys.VirtualMachine.Test.TestUtils;
using Xunit;

namespace Soltys.VirtualMachine.Test
{
    public partial class InstructionDecoderTests
    {

        [Fact]
        public void Decode_ZeroBytesToDecode_RaiseArgumentException()
        {
            Assert.Throws<ArgumentException>(() => InstructionDecoder.Decode(Array.Empty<byte>()));
        }

        [Theory]
        [InlineData(Opcode.Undefined)]
        [InlineData(Opcode.Custom)]
        public void Decode_NotSupportedOpcode_ThrowOpcodeReadException(Opcode opcode)
        {
            var bytes = InstructionByteBuilder.Create().Opcode(opcode).ToArray();
            Assert.Throws<OpcodeDecodeException>(() => InstructionDecoder.Decode(bytes));
        }

        [Fact]
        public void Decode_StoreInstructionFromByteArray_RegistryIndexAndStoreTypeAreSet()
        {
            const StoreType expectedStoreType = StoreType.Field;
            const byte expectedIndex = 0x05;
            var bytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Store, expectedStoreType, expectedIndex)
                .ToArray();

            var storeInstruction = AssertBytesDecodedAs<StoreInstruction>(bytes);

            Assert.Equal(expectedIndex, storeInstruction.Index);
            Assert.Equal(expectedStoreType, storeInstruction.StoreType);
        }

        [Fact]
        public void Decode_LoadPlaceInstruction_FromByteArray_RegistryIndexAndLoadTypeAreSet()
        {
            const LoadType expectedLoadType = LoadType.StaticField;
            const byte expectedIndex = 0x07;
            var bytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, expectedLoadType, expectedIndex)
                .ToArray();

            var loadInstruction = AssertBytesDecodedAs<LoadPlaceInstruction>(bytes);

            Assert.Equal(expectedIndex, loadInstruction.Index);
            Assert.Equal(expectedLoadType, loadInstruction.LoadType);
        }

        [Fact]
        public void Decode_LoadString_FromByteArray()
        {
            const string value = "helloWorld";
            var bytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, LoadType.String, value)
                .ToArray();

            var loadInstruction = AssertBytesDecodedAs<LoadStringInstruction>(bytes);

            Assert.Equal(LoadType.String, loadInstruction.LoadType);
            Assert.Equal(value, loadInstruction.Value);
        }

        [Theory]
        [InlineData(LoadType.Integer, 42)]
        [InlineData(LoadType.Double, 42.69)]
        public void Decode_LoadConstantInstruction_FromByteArray(LoadType loadType, object expectedValue)
        {
            var bytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, loadType, expectedValue)
                .ToArray();

            var loadInstruction = AssertBytesDecodedAs<LoadConstantInstruction>(bytes);

            Assert.Equal(loadType, loadInstruction.LoadType);
            Assert.Equal(expectedValue, loadInstruction.Value);
        }

        [Theory]
        [InlineData(CompareType.Equals)]
        [InlineData(CompareType.GreaterThan)]
        [InlineData(CompareType.LessThan)]
        public void Decode_CompareInstruction_FromBytesArray(CompareType compareType)
        {
            var bytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Compare, compareType)
                .ToArray();
            var compareInstruction = AssertBytesDecodedAs<CompareInstruction>(bytes);
            Assert.Equal(compareType, compareInstruction.CompareType);
        }

        [Theory]
        [InlineData(BranchType.Jump)]
        [InlineData(BranchType.IfFalse)]
        [InlineData(BranchType.IfTrue)]
        public void Decode_BranchInstruction_FromByteArray(BranchType branchType)
        {
            var bytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Branch, branchType, 42)
                .ToArray();

            var branchInstruction = AssertBytesDecodedAs<BranchInstruction>(bytes);

            Assert.Equal(branchType, branchInstruction.BranchType);
        }

        [Fact]
        public void Decode_CallInstruction_FromByteArray()
        {
            const string methodCall = "void HelloWorld()";
            var bytes = InstructionByteBuilder.Create()
                .Opcode(Opcode.Call, methodCall)
                .ToArray();

            var callInstruction = AssertBytesDecodedAs<CallInstruction>(bytes);
            Assert.Equal(methodCall, callInstruction.MethodCall);
        }

        #region Decode Opcode without opperands

        [Fact]
        public void Decode_AddInstruction_FromByteArray() =>
            AssertCreatedInstruction<AddInstruction>(Opcode.Add);

        [Fact]
        public void Decode_SubtractInstruction_FromByteArray() =>
            AssertCreatedInstruction<SubtractionInstruction>(Opcode.Subtraction);

        [Fact]
        public void Decode_MultiplicationInstruction_FromByteArray() =>
            AssertCreatedInstruction<MultiplicationInstruction>(Opcode.Multiplication);

        [Fact]
        public void Decode_DivisionInstruction_FromByteArray() =>
            AssertCreatedInstruction<DivisionInstruction>(Opcode.Division);

        [Fact]
        public void Decode_NopInstruction_FromByteArray() =>
            AssertCreatedInstruction<NopInstruction>(Opcode.Nop);

        [Fact]
        public void Decode_ReturnInstruction_FromByteArray() =>
            AssertCreatedInstruction<ReturnInstruction>(Opcode.Return);

        private static void AssertCreatedInstruction<TInstruction>(Opcode opcode) where TInstruction : IInstruction
        {
            var bytes = InstructionByteBuilder.Create()
                .Opcode(opcode)
                .ToArray();
            AssertBytesDecodedAs<TInstruction>(bytes);
        }

        #endregion

        private static TInstruction AssertBytesDecodedAs<TInstruction>(byte[] bytes) where TInstruction : IInstruction
        {
            var (instruction, bytesRead) = InstructionDecoder.Decode(bytes);

            Assert.IsType<TInstruction>(instruction);
            var loadInstruction = (TInstruction)instruction;

            //Whole message was read
            Assert.Equal(bytes.Length, bytesRead);
            return loadInstruction;
        }
    }
}
