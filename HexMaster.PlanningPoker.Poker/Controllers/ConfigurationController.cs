using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.BuildingBlocks.EventBus.Configuration;
using HexMaster.Helpers.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HexMaster.PlanningPoker.Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        public IOptions<MongoDbSettings> MongoSettings { get; }
        public IOptions<EventBusSettings> ServicebusSettings { get; }

        public IActionResult Get()
        {
            return Ok(new
            {
                azureServiceBus = ServicebusSettings.Value.AzureServiceBusEnabled,
                azureServiceBusAddress = ServicebusSettings.Value.EventBusConnection,
                documentServer = MongoSettings.Value.Hostname,
                documentDatabase = MongoSettings.Value.DatabaseName
            });
        }


        public ConfigurationController(IOptions<MongoDbSettings> mongoSettings, IOptions<EventBusSettings> servicebusSettings)
        {
            MongoSettings = mongoSettings;
            ServicebusSettings = servicebusSettings;
        }
    }
}