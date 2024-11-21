using WebApplication2.Data;
using WebApplication2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();

        public async Task<Employee> GetAsync(int id) => await _context.Employees.FindAsync(id);

        public async Task<Employee> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(employee.Id);
            if (existingEmployee == null) return null;

            existingEmployee.EmployeeId = employee.EmployeeId;
            existingEmployee.Position = employee.Position;
            existingEmployee.Hours = employee.Hours;
            existingEmployee.ContactInfo = employee.ContactInfo;
            existingEmployee.EnterpriseId = employee.EnterpriseId;
            await _context.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}