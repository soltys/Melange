using System;

namespace Soltys.Page
{
    internal class ClientConfigurationProvider : IClientConfigurationProvider
    {
        public ClientConfiguration Get()
        {
            return new ClientConfiguration() 
            {
                Backend = new BackendConfiguration() 
                {
                    IsOn = true,
                    IsInternal = GetEnvValue("PAGE_ISINTERNAL", false)
                }
            };
        }

        private bool GetEnvValue(string envVariableName, bool fallbackValue)
        {
            var envValue = Environment.GetEnvironmentVariable(envVariableName);
            if (envValue == null)
            {
                return fallbackValue;
            }

            if (!bool.TryParse(envValue, out var value))
            {
                return fallbackValue;
            }

            return value;
        }
    }
}
