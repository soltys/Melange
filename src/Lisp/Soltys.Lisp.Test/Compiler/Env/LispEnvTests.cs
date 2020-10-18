using System;
using System.Collections.Generic;
using Soltys.Lisp.Compiler;
using Soltys.VirtualMachine.Contracts;
using Xunit;

namespace Soltys.Lisp.Test.Compiler
{
    public class LispEnvTests
    {
        [Fact]
        public void Eval_Defines_ValueReferenced()
        {
            var env = new LispEnv();
            var name = new AstSymbol("x");
            var value = new AstIntNumber(42);

            env.Eval(new AstList(
                new AstSymbol("def!"),
                name,
                value));
            var data = env.Copy();
            Assert.Equal(value, data.Defines[name]);
        }

        [Fact]
        public void VisitLibrary_SavesFunctionNamesInEnv()
        {
            var env = new LispEnv();
            env.VisitLibrary(new SimpleTestCoreLibrary());

            var data = env.Copy();
            Assert.Contains("a", data.CoreFunctionNames);
            Assert.Contains("b", data.CoreFunctionNames);
            Assert.Contains("c", data.CoreFunctionNames);
        }

        private class SimpleTestCoreLibrary : IVMLibrary
        {
            public IReadOnlyDictionary<string, IVMExternalFunction> Functions
            {
                get;
            }

            public SimpleTestCoreLibrary()
            {
                var emptyAction = new Action(() => { });
                Functions = new Dictionary<string, IVMExternalFunction>
                {
                    ["a"] = new VMExternalFunction(emptyAction),
                    ["b"] = new VMExternalFunction(emptyAction),
                    ["c"] = new VMExternalFunction(emptyAction)
                };
            }
        }
    }
}
