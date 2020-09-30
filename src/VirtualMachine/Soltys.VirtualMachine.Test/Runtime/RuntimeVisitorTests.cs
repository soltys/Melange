using System;
using System.Collections.Generic;
using Soltys.VirtualMachine.Contracts;
using Xunit;

namespace Soltys.VirtualMachine.Test.Runtime
{
    public class RuntimeVisitorTests
    {
        private const string CorrectOrderVMFunction = "correctOrder";
        [Fact]
        public void VisitCall_CallsMethodWithCorrectOrderOfParametersPulledFromStack()
        {
            var testVmContext = new TestVmContext();

            testVmContext.ValueStack.Push(1);
            testVmContext.ValueStack.Push(2);
            testVmContext.ValueStack.Push(3);

            var sut = new RuntimeVisitor(testVmContext);
            
            sut.VisitCall(new CallInstruction(RuntimeVisitorTests.CorrectOrderVMFunction));
        }

        [Fact]
        public void VisitSubtraction_DoesSubtraction_ResultPushedToValueStack()
        {
            var testVmContext = new TestVmContext();

            testVmContext.ValueStack.Push(10);
            testVmContext.ValueStack.Push(7);

            var sut = new RuntimeVisitor(testVmContext);

            sut.VisitSubtraction(new SubtractionInstruction());

            Assert.Equal(3, testVmContext.ValueStack.Peek());
        }

        [Fact]
        public void VisitSubtraction_DoesSubtractionWithNegativeResult_ResultPushedToValueStack()
        {
            var testVmContext = new TestVmContext();
            
            testVmContext.ValueStack.Push(7);
            testVmContext.ValueStack.Push(10);

            var sut = new RuntimeVisitor(testVmContext);

            sut.VisitSubtraction(new SubtractionInstruction());

            Assert.Equal(-3, testVmContext.ValueStack.Peek());
        }

        public class TestVmContext : IVMContext
        {
            public Stack<object> ValueStack
            {
                get;
            }= new Stack<object>();

            public void AddVMLibrary(IVMLibrary library)
            {
            }

            public IVMExternalFunction FindExternalFunction(string methodName)
            {
                switch (methodName)
                {
                    case RuntimeVisitorTests.CorrectOrderVMFunction:
                    default:
                        return new VMExternalFunction(new Action<int, int, int>((i1, i2, i3) =>
                        {
                            Assert.Equal(1, i1);
                            Assert.Equal(2, i2);
                            Assert.Equal(3, i3);
                        }));
                }
            }

            public bool TryChangeFunction(string methodName) => false;
            public void Return()
            {
            }
        }
    }

    
}
