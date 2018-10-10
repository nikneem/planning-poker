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
    public class EstimationsController : ControllerBase
    {
        public IPokerSessionsService Service { get; }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PokerEstimationDto dto)
        {
            try
            {
                var response = await Service.Estimate(dto);
                if (response)
                {
                    return Accepted(dto.Estimation);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public EstimationsController(IPokerSessionsService service)
        {
            Service = service;
        }

    }
}