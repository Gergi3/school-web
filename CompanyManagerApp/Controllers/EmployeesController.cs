using CompanyManagerApp.Data;
using CompanyManagerApp.Models.Domain;
using CompanyManagerApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        List<Department> departments = await this._context.Departments.ToListAsync();

        var departmentListItems = departments
            .Select(x => new { x.Id, x.Name })
            .ToHashSet();

        this.ViewBag.DepartmentsList = new SelectList(departmentListItems, "Id", "Name");
        return await Task.Run(() => View());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeViewModel employeeViewModel)
    {
        Employee employee = new Employee()
        {
            Id = new Guid(),
            Name = employeeViewModel.Name,
            Email = employeeViewModel.Email,
            Salary = employeeViewModel.Salary,
            DateOfBirth = employeeViewModel.DateOfBirth,
            DepartmentId = employeeViewModel.DepartmentId,
        };

        await this._context.Employees.AddAsync(employee);
        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }
}
