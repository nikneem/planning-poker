using System;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Refinements.Contracts.Services;
using HexMaster.PlanningPoker.Refinements.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace HexMaster.PlanningPoker.Refinements.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefinementsController : ControllerBase
    {
        private readonly IRefinementsService _service;

        public RefinementsController(IRefinementsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Create(string code)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RefinementDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var response = await _service.Create(dto);
                if (response == null)
                {
                    return BadRequest();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

    }
}