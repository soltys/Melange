using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Soltys.Page.Controllers
{
    [ApiController]
    [Route("configuration.js")]
    public class ConfigurationController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            var config = new ClientConfiguration
            {
                Backend = new BackendConfiguration
                {
                    IsOn = true,
                    IsInternal = true
                }
            };

            var configString = JsonSerializer.Serialize(config, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            return $"const clientConfig = {configString};";
        }
    }

    public class ClientConfiguration
    {
        public BackendConfiguration Backend
        {
            get;
            set;
        }
    }

    public class BackendConfiguration
    {
        public bool IsOn
        {
            get;
            set;
        }

        public bool IsInternal
        {
            get;
            set;
        }

    }
}
