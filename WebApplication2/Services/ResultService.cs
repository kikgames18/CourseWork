using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class ResultService
    {
        private readonly ApplicationDbContext _context;

        public ResultService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Result>> GetAllAsync()
        {
            return await _context.Results.ToListAsync();
        }

        public async Task<Result> GetByIdAsync(int id)
        {
            return await _context.Results.FindAsync(id);
        }

        public async Task CreateAsync(Result result)
        {
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Result result)
        {
            _context.Results.Update(result);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result != null)
            {
                _context.Results.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}