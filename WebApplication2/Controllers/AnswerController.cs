using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly AnswerService _answerService;

        public AnswerController(AnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var answers = await _answerService.GetAllAsync();
            return Ok(answers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var answer = await _answerService.GetByIdAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            return Ok(answer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Answer answer)
        {
            await _answerService.CreateAsync(answer);
            return CreatedAtAction(nameof(GetById), new { id = answer.ID }, answer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Answer answer)
        {
            if (id != answer.ID)
            {
                return BadRequest();
            }

            await _answerService.UpdateAsync(answer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _answerService.DeleteAsync(id);
            return NoContent();
        }
    }
}