using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var questions = await _questionService.GetAllAsync();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var question = await _questionService.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Question question)
        {
            await _questionService.CreateAsync(question);
            return CreatedAtAction(nameof(GetById), new { id = question.ID }, question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Question question)
        {
            if (id != question.ID)
            {
                return BadRequest();
            }

            await _questionService.UpdateAsync(question);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _questionService.DeleteAsync(id);
            return NoContent();
        }
    }
}