using CompanyManagerApp.Data;
using CompanyManagerApp.Models.Domain;
using CompanyManagerApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagerApp.Controllers;

public class EmployeesController : Controller
{
    private readonly CompanyManagerAppContext _context;

    public EmployeesController(CompanyManagerAppContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Employee> employees = await this._context.Employees
            .Include(e => e.Department)
            .ToListAsync();

        HashSet<IndexEmployeeViewModel> employeesViewModel = employees
            .Select(x => new IndexEmployeeViewModel()
            {
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                Id = x.Id,
                Name = x.Name,
                Salary = x.Salary,
                DepartmentName = x.Department.Name
            })
            .ToHashSet();

        return await Task.Run(() => View(employeesViewModel));
    }
}
