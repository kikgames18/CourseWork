using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class SurveyService
    {
        private readonly ApplicationDbContext _context;

        public SurveyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Survey>> GetAllAsync()
        {
            return await _context.Surveys.ToListAsync();
        }

        public async Task<Survey> GetByIdAsync(int id)
        {
            return await _context.Surveys.FindAsync(id);
        }

        public async Task CreateAsync(Survey survey)
        {
            _context.Surveys.Add(survey);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Survey survey)
        {
            _context.Surveys.Update(survey);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var survey = await _context.Surveys.FindAsync(id);
            if (survey != null)
            {
                _context.Surveys.Remove(survey);
                await _context.SaveChangesAsync();
            }
        }
    }
}