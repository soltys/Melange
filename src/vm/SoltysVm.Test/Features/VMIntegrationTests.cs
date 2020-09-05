using System.IO;
using SoltysVm.Test.TestUtils;
using Xunit;

namespace SoltysVm.Test
{
    public class VMIntegrationTests
    {
        [Fact]
        public void Run_ExecuteInstructions_VisitingInstructionMethods()
        {
            var bytesStream = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, LoadType.Integer, 69)
                .Opcode(Opcode.Load, LoadType.Integer, 42)
                .Opcode(Opcode.Add)
                .AsStream();
            var callCountingRuntime = new CallCountingRuntime();
            var vm = new VM(callCountingRuntime);

            vm.Load(bytesStream);
            vm.Run();

            Assert.Equal(2, callCountingRuntime.CallCount[nameof(IRuntimeVisitor.VisitLoadConstant)]);
            Assert.Equal(1, callCountingRuntime.CallCount[nameof(IRuntimeVisitor.VisitAdd)]);
        }

        [Theory]
        [InlineData(Opcode.Add, 111)]
        [InlineData(Opcode.Subtraction, 27)]
        [InlineData(Opcode.Multiplication, 2898)]
        [InlineData(Opcode.Division, 1)]
        public void Run_MathIntOperation_ResultIsCorrect(Opcode opcode, int expectedValue)
        {
            var bytesStream = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, LoadType.Integer, 69)
                .Opcode(Opcode.Load, LoadType.Integer, 42)
                .Opcode(opcode)
                .AsStream();
            AssertIntValue(bytesStream, expectedValue);
        }

        [Theory]
        [InlineData(Opcode.Add, 111.2)]
        [InlineData(Opcode.Subtraction, 27.0)]
        [InlineData(Opcode.Multiplication, 2909.11)]
        [InlineData(Opcode.Division, 1.641330166270784)]
        public void Run_MathFloatingPointOperation_ResultIsCorrect(Opcode opcode, double expectedValue)
        {
            var bytesStream = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, LoadType.Double, 69.1)
                .Opcode(Opcode.Load, LoadType.Double, 42.1)
                .Opcode(opcode)
                .AsStream();
            AssertDoubleValue(bytesStream, expectedValue);
        }

        [Fact]
        public void Run_MathOperationChain_ResultIsCorrect()
        {
            var bytesStream = InstructionByteBuilder.Create()
                .Opcode(Opcode.Load, LoadType.Integer, 2)
                .Opcode(Opcode.Load, LoadType.Integer, 2)
                .Opcode(Opcode.Multiplication)
                .Opcode(Opcode.Load, LoadType.Integer, 2)
                .Opcode(Opcode.Add)
                .AsStream();
            AssertIntValue(bytesStream, 6);
        }

        private static void AssertIntValue(Stream bytesStream, int expectedValue) 
        {
            var lastValue = AssertStackValueType<int>(bytesStream);
            var intValue = (int) lastValue;
            Assert.Equal(expectedValue, intValue);
        }

        private static void AssertDoubleValue(Stream bytesStream, double expectedValue)
        {
            var lastValue = AssertStackValueType<double>(bytesStream);
            var doubleValue = (double)lastValue;
            Assert.Equal(expectedValue, doubleValue, 10);
        }

        private static object AssertStackValueType<TValue>(Stream bytesStream)
        {
            var vm = new VM(new RuntimeFactory());
            vm.Load(bytesStream);
            vm.Run();

            var lastValue = vm.PeekValueStack;
            Assert.IsType<TValue>(lastValue);
            return lastValue;
        }
    }
}
