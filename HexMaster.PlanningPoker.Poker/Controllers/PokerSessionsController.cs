using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Poker.Contracts.Services;
using HexMaster.PlanningPoker.Poker.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace HexMaster.PlanningPoker.Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokerSessionsController : ControllerBase
    {
        public IPokerSessionsService Service { get; }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PokerSessionCreateRequestDto model)
        {
            try
            {
                var result = await Service.Create(model);
                if (result == null)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        [HttpPost("join")]
        public async Task<IActionResult> Join([FromBody] PokerSessionJoinRequestDto model)
        {
            try
            {
                var result = await Service.Join(model);
                if (result == null)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }


        public PokerSessionsController(IPokerSessionsService service)
        {
            Service = service;
        }
    }
}