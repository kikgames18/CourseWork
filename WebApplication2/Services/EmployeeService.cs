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
            // Проверка на уникальность id, если у тебя вручную задается Id
            var existingEmployee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == employee.Id);
            if (existingEmployee != null)
            {
                throw new Exception("Employee with the same ID already exists.");
            }

            // Удаляем установку ID, если используется автоинкремент
            // employee.Id = 0; // Если нужно явно сбросить Id для автоинкремента

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

            // Обновляем свойства
            existingEmployee.EmployeeId = employee.EmployeeId;
            existingEmployee.Position = employee.Position;
            existingEmployee.Hours = employee.Hours;
            existingEmployee.ContactInfo = employee.ContactInfo;
            existingEmployee.EnterpriseId = employee.EnterpriseId;

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
