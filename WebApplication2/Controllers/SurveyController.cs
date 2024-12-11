using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly SurveyService _surveyService;

        public SurveyController(SurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var surveys = await _surveyService.GetAllAsync();
            return Ok(surveys);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var survey = await _surveyService.GetByIdAsync(id);
            if (survey == null)
            {
                return NotFound();
            }
            return Ok(survey);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Survey survey)
        {
            await _surveyService.CreateAsync(survey);
            return CreatedAtAction(nameof(GetById), new { id = survey.ID }, survey);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Survey survey)
        {
            if (id != survey.ID)
            {
                return BadRequest();
            }

            await _surveyService.UpdateAsync(survey);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _surveyService.DeleteAsync(id);
            return NoContent();
        }
    }
}