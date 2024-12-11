using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly ResultService _resultService;

        public ResultController(ResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _resultService.GetAllAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _resultService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Result result)
        {
            await _resultService.CreateAsync(result);
            return CreatedAtAction(nameof(GetById), new { id = result.ID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Result result)
        {
            if (id != result.ID)
            {
                return BadRequest();
            }

            await _resultService.UpdateAsync(result);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _resultService.DeleteAsync(id);
            return NoContent();
        }
    }
}