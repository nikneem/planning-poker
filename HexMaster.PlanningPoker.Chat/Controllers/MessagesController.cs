using System;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Chat.Contracts.Services;
using HexMaster.PlanningPoker.Chat.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace HexMaster.PlanningPoker.Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {

        public IChatService Service { get; }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddChatMessageDto dto)
        {
            try
            {
                var created = await Service.AddMessage(dto);
                if (created)
                {
                    return Accepted();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public MessagesController(IChatService service)
        {
            Service = service;
        }


    }
}