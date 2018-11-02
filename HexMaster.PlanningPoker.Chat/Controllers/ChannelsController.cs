using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Chat.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexMaster.PlanningPoker.Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController : ControllerBase
    {
        public IChatService Service { get; }

        [HttpGet("{channelId:guid}")]
        public async Task<IActionResult> Get([FromRoute]Guid channelId, [FromQuery]Guid participantId)
        {
            try
            {
                var channel = await Service.Get(channelId, participantId);
                if (channel == null)
                {
                    return BadRequest();
                }

                return Ok(channel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public ChannelsController(IChatService service)
        {
            Service = service;
        }

    }
}