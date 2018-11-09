using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Poker.Contracts.Services;
using HexMaster.PlanningPoker.Poker.DataTransferObjects;
using HexMaster.PlanningPoker.Poker.Infrastructure.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HexMaster.PlanningPoker.Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokerSessionsController : ControllerBase
    {
        public IPokerSessionsService Service { get; }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                // ID Must be two guids
                if (id?.Length == 72)
                {
                    if (Guid.TryParse(id.Substring(0, 36), out Guid sessionId))
                    {
                        if (Guid.TryParse(id.Substring(36, 36), out Guid participantId))
                        {
                            var session = await Service.Get(sessionId, participantId);
                            return Ok(session);
                        }
                    }
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpGet("{pokerSessionId:guid}/start")]
        public async Task<IActionResult> Start([FromRoute] Guid pokerSessionId)
        {
            try
            {
                var result = await Service.Start(pokerSessionId);
                if (!result)
                {
                    return BadRequest();
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        [HttpGet("{pokerSessionId:guid}/reset")]
        public async Task<IActionResult> Reset([FromRoute] Guid pokerSessionId)
        {
            try
            {
                var result = await Service.Reset(pokerSessionId);
                if (!result)
                {
                    return BadRequest();
                }

                return Ok(true);
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
                if (model.ControlType == ControlType.All)
                {
                    model.ControlType = ControlType.Shared;
                }
                if (model.ControlType == ControlType.Self)
                {
                    model.ControlType = ControlType.Individual;
                }
                if (model.StartType == StartType.Immidiate)
                {
                    model.StartType = StartType.Automatically;
                }
                if (model.StartType == StartType.Delayed)
                {
                    model.StartType = StartType.Manually;
                }
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

                return Ok(true);
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