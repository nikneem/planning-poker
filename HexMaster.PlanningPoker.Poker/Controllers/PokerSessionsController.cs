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

        [HttpGet("{pokerSessionId:guid}/start")]
        public async Task<IActionResult> Start([FromQuery] Guid pokerSessionId)
        {
            try
            {
                var result = await Service.Start(pokerSessionId);
                if (!result)
                {
                    return BadRequest();
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpGet("{pokerSessionId:guid}/reset")]
        public async Task<IActionResult> Reset([FromQuery] Guid pokerSessionId)
        {
            try
            {
                var result = await Service.Reset(pokerSessionId);
                if (!result)
                {
                    return BadRequest();
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

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

        [HttpPost("leave")]
        public async Task<IActionResult> Leave([FromBody] PokerSessionLeaveRequestDto model)
        {
            try
            {
                var result = await Service.Leave(model);
                if (!result)
                {
                    return BadRequest();
                }

                return Accepted();
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