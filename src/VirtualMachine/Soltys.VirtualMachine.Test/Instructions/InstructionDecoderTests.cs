using Soltys.VirtualMachine.Test.TestUtils;
using Xunit;

namespace Soltys.VirtualMachine.Test;

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
        const StoreKind expectedStoreType = StoreKind.Field;
        const byte expectedIndex = 0x05;
        var bytes = InstructionByteBuilder.Create()
            .Opcode(Opcode.Store, expectedStoreType, expectedIndex)
            .ToArray();

        var storeInstruction = AssertBytesDecodedAs<StoreInstruction>(bytes);

        Assert.Equal(expectedIndex, storeInstruction.Index);
        Assert.Equal(expectedStoreType, storeInstruction.StoreKind);
    }

    [Fact]
    public void Decode_LoadPlaceInstruction_FromByteArray_RegistryIndexAndLoadTypeAreSet()
    {
        const LoadKind expectedLoadType = LoadKind.StaticField;
        const byte expectedIndex = 0x07;
        var bytes = InstructionByteBuilder.Create()
            .Opcode(Opcode.Load, expectedLoadType, expectedIndex)
            .ToArray();

        var loadInstruction = AssertBytesDecodedAs<LoadPlaceInstruction>(bytes);

        Assert.Equal(expectedIndex, loadInstruction.Index);
        Assert.Equal(expectedLoadType, loadInstruction.LoadKind);
    }

    [Fact]
    public void Decode_LoadString_FromByteArray()
    {
        const string value = "helloWorld";
        var bytes = InstructionByteBuilder.Create()
            .Opcode(Opcode.Load, LoadKind.String, value)
            .ToArray();

        var loadInstruction = AssertBytesDecodedAs<LoadStringInstruction>(bytes);

        Assert.Equal(LoadKind.String, loadInstruction.LoadKind);
        Assert.Equal(value, loadInstruction.Value);
    }

    [Theory]
    [InlineData(LoadKind.Integer, 42)]
    [InlineData(LoadKind.Double, 42.69)]
    public void Decode_LoadConstantInstruction_FromByteArray(LoadKind loadKind, object expectedValue)
    {
        var bytes = InstructionByteBuilder.Create()
            .Opcode(Opcode.Load, loadKind, expectedValue)
            .ToArray();

        var loadInstruction = AssertBytesDecodedAs<LoadConstantInstruction>(bytes);

        Assert.Equal(loadKind, loadInstruction.LoadKind);
        Assert.Equal(expectedValue, loadInstruction.Value);
    }

    [Fact]
    public void Decode_LoadLibraryInstruction_FromByteArray()
    {
        var bytes = InstructionByteBuilder.Create()
            .Opcode(Opcode.Load, LoadKind.Library, "MyLibraryCore")
            .ToArray();

        var loadInstruction = AssertBytesDecodedAs<LoadLibraryInstruction>(bytes);

        Assert.Equal(LoadKind.Library, loadInstruction.LoadKind);
        Assert.Equal("MyLibraryCore", loadInstruction.LibraryName);
    }


    [Theory]
    [InlineData(CompareKind.Equals)]
    [InlineData(CompareKind.GreaterThan)]
    [InlineData(CompareKind.LessThan)]
    public void Decode_CompareInstruction_FromBytesArray(CompareKind compareKind)
    {
        var bytes = InstructionByteBuilder.Create()
            .Opcode(Opcode.Compare, compareKind)
            .ToArray();
        var compareInstruction = AssertBytesDecodedAs<CompareInstruction>(bytes);
        Assert.Equal(compareKind, compareInstruction.CompareKind);
    }

    [Theory]
    [InlineData(BranchKind.Jump)]
    [InlineData(BranchKind.IfFalse)]
    [InlineData(BranchKind.IfTrue)]
    public void Decode_BranchInstruction_FromByteArray(BranchKind branchKind)
    {
        var bytes = InstructionByteBuilder.Create()
            .Opcode(Opcode.Branch, branchKind, 42)
            .ToArray();

        var branchInstruction = AssertBytesDecodedAs<BranchInstruction>(bytes);

        Assert.Equal(branchKind, branchInstruction.BranchKind);
    }

    [Fact]
    public void Decode_CallInstruction_FromByteArray()
    {
        const string methodCall = "void HelloWorld()";
        var bytes = InstructionByteBuilder.Create()
            .Opcode(Opcode.Call, methodCall)
            .ToArray();

        var callInstruction = AssertBytesDecodedAs<CallInstruction>(bytes);
        Assert.Equal(methodCall, callInstruction.MethodName);
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

    [Fact]
    public void Decode_ListNewInstruction_FromByteArray() =>
        AssertCreatedInstruction<ListNewInstruction>(Opcode.List, ListOperationKind.New);

    [Fact]
    public void Decode_ListAddInstruction_FromByteArray() =>
        AssertCreatedInstruction<ListAddInstruction>(Opcode.List, ListOperationKind.Add);

    private static void AssertCreatedInstruction<TInstruction>(Opcode opcode, params object[] args) where TInstruction : IInstruction
    {
        var bytes = InstructionByteBuilder.Create()
            .Opcode(opcode, args)
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
