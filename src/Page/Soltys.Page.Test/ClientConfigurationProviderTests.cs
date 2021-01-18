using System;
using Xunit;

namespace Soltys.Page.Test
{
    public class ClientConfigurationProviderTests
    {
        [Fact]
        public void BackendIsOn_Always_IsTrue()
        {
            var config = GetConfig();
            Assert.True(config.Backend.IsOn);
        }

        [Fact]
        public void BackendIsInternal_ByDefault_IsFalse()
        {
            var config = GetConfig();
            Assert.False(config.Backend.IsInternal);
        }

        [Fact]
        public void BackendIsInternal_EnvironmentVariableIsSetToTrue_IsTrue()
        {
            using (new TestEnvVariable("PAGE_ISINTERNAL", "TRUE"))
            {
                var config = GetConfig();
                Assert.True(config.Backend.IsInternal);
            }
        }

        [Fact]
        public void BackendIsInternal_EnvironmentVariableIsSetToGarbage_IsFalse()
        {
            using (new TestEnvVariable("PAGE_ISINTERNAL", "xxx"))
            {
                var config = GetConfig();
                Assert.False(config.Backend.IsInternal);
            }
        }

        private static ClientConfiguration GetConfig()
        {
            var sut = new ClientConfigurationProvider();
            var config = sut.Get();
            return config;
        }

        class TestEnvVariable : IDisposable
        {
            private readonly string variableName;

            public TestEnvVariable(string envVariableName, string value)
            {
                this.variableName = envVariableName;
                Environment.SetEnvironmentVariable(this.variableName, value);
            }

            public void Dispose()
            {
                Environment.SetEnvironmentVariable(this.variableName, string.Empty);
            }
        }
    }
}
