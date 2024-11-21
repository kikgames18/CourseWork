using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;
using WebApplication2.Models;
using System.Threading.Tasks;

public class EmployeeController : Controller
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // Отображение списка сотрудников на странице
    public async Task<IActionResult> Index()
    {
        var employees = await _employeeService.GetAllAsync();
        return View(employees);
    }

    // API для получения списка сотрудников в формате JSON
    [HttpGet("api/employees")]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _employeeService.GetAllAsync();
        return Json(employees); // Возвращаем список сотрудников как JSON
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeService.GetAsync(id);
        if (employee == null) return NotFound();

        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Employee updatedEmployee)
    {
        await _employeeService.UpdateAsync(updatedEmployee);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _employeeService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee newEmployee)
    {
        await _employeeService.AddAsync(newEmployee);
        return RedirectToAction(nameof(Index));
    }
}
