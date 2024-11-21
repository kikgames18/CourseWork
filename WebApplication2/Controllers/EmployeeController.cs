using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var employees = await _context.Employees.ToListAsync();
                if (employees == null || !employees.Any())
                {
                    return NotFound("No employees found.");
                }

                // Логируем JSON-данные в консоль для проверки
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(employees));

                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromQuery] int id, [FromBody] Employee updatedEmployee)
        {
            try
            {
                var existingEmployee = await _context.Employees.FindAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound("Employee not found.");
                }

                existingEmployee.EmployeeId = updatedEmployee.EmployeeId;
                existingEmployee.Position = updatedEmployee.Position;
                existingEmployee.Hours = updatedEmployee.Hours;
                existingEmployee.ContactInfo = updatedEmployee.ContactInfo;
                existingEmployee.EnterpriseId = updatedEmployee.EnterpriseId;

                await _context.SaveChangesAsync();

                return Ok("Employee updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound("Employee not found.");
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return Ok("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Employee newEmployee)
        {
            try
            {
                await _context.Employees.AddAsync(newEmployee);
                await _context.SaveChangesAsync();

                return Ok("Employee created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}