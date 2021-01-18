using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Soltys.Page
{
    [ApiController]
    [Route("configuration.js")]
    public class ConfigurationController : ControllerBase
    {
        private IClientConfigurationProvider configurationProvider;

        public ConfigurationController(IClientConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
        }

        [HttpGet]
        public string Get()
        {
            var config = this.configurationProvider.Get();
            var configString = JsonSerializer.Serialize(config, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            return $"const clientConfig = {configString};";
        }
    }
}
