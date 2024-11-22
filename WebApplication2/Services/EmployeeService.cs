using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using System;

namespace WebApplication2.Services
{
    public class EmployeeService
    {
        // Используем ApplicationDbContext, который у тебя существует
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        // CRUD операции:

        // Получение всех сотрудников
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        // Получение сотрудника по Id
        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        // Создание нового сотрудника
        public async Task CreateAsync(Employee employee)
        {
            // Check if the employee with the same Id already exists
            var existingEmployee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == employee.Id);
            if (existingEmployee != null)
            {
                // If the employee exists, update it instead of creating a new one
                await UpdateAsync(employee);
                return;
            }

            // Create a new employee
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }


        // Обновление информации о сотруднике
        public async Task UpdateAsync(Employee employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(employee.Id);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found");
            }

            existingEmployee.EmployeeId = employee.EmployeeId;
            existingEmployee.Position = employee.Position;
            existingEmployee.Hours = employee.Hours;
            existingEmployee.ContactInfo = employee.ContactInfo;
            existingEmployee.EnterpriseId = employee.EnterpriseId;

            // If the Id is changed, update it
            if (existingEmployee.Id != employee.Id)
            {
                existingEmployee.Id = employee.Id;
            }

            await _context.SaveChangesAsync();
        }


        // Удаление сотрудника по Id
        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
