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
    public async Task<IActionResult> Index(string? department = null)
    {
        List<Employee> employees = await this._context.Employees
            .Include(e => e.Department)
            .Where(x => department == null ? true : x.Department.Code == department)
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
    public async Task<IActionResult> View(Guid id)
    {
        var employee = await this._context.Employees.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);

        if (employee is null)
        {
            return await Task.Run(() => NotFound());
        }

        var employeeViewModel = new ViewEmployeeViewModel()
        {
            DateOfBirth = employee.DateOfBirth,
            Email = employee.Email,
            DepartmentName = employee.Department.Name,
            Name = employee.Name,
            Salary = employee.Salary,
            Id = employee.Id
        };

        return this.View(employeeViewModel);
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

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        Employee? employee = await this._context.Employees.FindAsync(id);

        if (employee is null)
        {
            return await Task.Run(() => NotFound());
        }

        EditEmployeeViewModel employeeViewModel = new EditEmployeeViewModel()
        {
            Id = employee.Id,
            Email = employee.Email,
            Salary = employee.Salary,
            DateOfBirth = employee.DateOfBirth,
            Name = employee.Name,
            DepartmentId = employee.DepartmentId
        };

        List<Department> departments = await this._context.Departments.ToListAsync();
        var departmentListItems = departments
            .Select(x => new { x.Id, x.Name })
            .ToHashSet();

        this.ViewBag.DepartmentsList = new SelectList(departmentListItems, "Id", "Name", employee.DepartmentId);
        return await Task.Run(() => View(employeeViewModel));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditEmployeeViewModel employeeViewModel)
    {
        Employee? employee = await this._context.Employees.FindAsync(employeeViewModel.Id);

        if (employee is null)
        {
            return await Task.Run(() => NotFound());
        }

        employee.Email = employeeViewModel.Email;
        employee.Salary = employeeViewModel.Salary;
        employee.DateOfBirth = employeeViewModel.DateOfBirth;
        employee.Name = employeeViewModel.Name;
        employee.DepartmentId = employeeViewModel.DepartmentId;

        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var employee = await this._context.Employees.FindAsync(id);
        if (employee is null)
        {
            return await Task.Run(() => NotFound());
        }

        this._context.Employees.Remove(employee);
        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }
}
