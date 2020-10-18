using System.Collections.Generic;
using Soltys.VirtualMachine.Test.TestUtils;
using Xunit;

namespace Soltys.VirtualMachine.Test
{
    public class InstructionVisitorCallTests
    {
        public static IEnumerable<object[]> AstObjectsSource =>
            new List<object[]>
            {
               new object [] {new SubtractionInstruction(), nameof(IRuntimeVisitor.VisitSubtraction)},
               new object [] {new AddInstruction(), nameof(IRuntimeVisitor.VisitAdd)},
               new object [] {new MultiplicationInstruction(), nameof(IRuntimeVisitor.VisitMultiplication)},
               new object [] {new DivisionInstruction(), nameof(IRuntimeVisitor.VisitDivision)},

               new object [] {new LoadLibraryInstruction("MyLib"), nameof(IRuntimeVisitor.VisitLoadLibrary)},

               new object[] {new BranchInstruction(BranchKind.Jump, 42), nameof(IRuntimeVisitor.VisitBranch) },
               new object[] {new BranchInstruction(BranchKind.IfTrue, 42), nameof(IRuntimeVisitor.VisitBranch) },
               new object[] {new BranchInstruction(BranchKind.IfFalse, 42), nameof(IRuntimeVisitor.VisitBranch) },

               new object[] {new ListNewInstruction(), nameof(IRuntimeVisitor.VisitList)},
               new object[] {new ListAddInstruction(), nameof(IRuntimeVisitor.VisitList)}
            };

        [Theory]
        [MemberData(nameof(AstObjectsSource))]
        internal void Correct_VisitorCallIsMadeByEachInstruction(IInstruction instruction, string expectedCalledMethod)
        {
            var visitorCallCounting = new CallCountingRuntime();
            instruction.Accept(visitorCallCounting);
            Assert.Single(visitorCallCounting.CallCount);
            Assert.Equal(1, visitorCallCounting.CallCount[expectedCalledMethod]);
        }
    }
}
