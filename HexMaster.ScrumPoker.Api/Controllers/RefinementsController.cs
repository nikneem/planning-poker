using System;
using System.Threading.Tasks;
using HexMaster.ScrumPoker.Api.Contracts.Services;
using HexMaster.ScrumPoker.Api.DataTransferObjects;
using HexMaster.ScrumPoker.Api.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace HexMaster.ScrumPoker.Api.Controllers
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