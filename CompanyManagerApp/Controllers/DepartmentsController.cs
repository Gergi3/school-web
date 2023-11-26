using CompanyManagerApp.Data;
using CompanyManagerApp.Models.Domain;
using CompanyManagerApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagerApp.Controllers;
public class DepartmentsController : Controller
{
    private readonly CompanyManagerAppContext _context;

    public DepartmentsController(CompanyManagerAppContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Department> departments = await this._context.Departments.ToListAsync();
        HashSet<IndexDepartmentViewModel> departmentsViewModels = departments
            .Select(x => new IndexDepartmentViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code
            })
            .ToHashSet();

        return await Task.Run(() => View(departmentsViewModels));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDepartmentViewModel departmentViewModel)
    {
        Department department = new Department()
        {
            Id = new Guid(),
            Name = departmentViewModel.Name,
            Code = departmentViewModel.Code
        };

        await this._context.Departments.AddAsync(department);
        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        Department? department = await this._context.Departments.FindAsync(id);
        if (department is null)
        {
            return await Task.Run(() => NotFound());
        }

        EditDepartmentViewModel departmentViewModel = new EditDepartmentViewModel()
        {
            Id = id,
            Name = department.Name,
            Code = department.Code,
        };

        return await Task.Run(() => View(departmentViewModel));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditDepartmentViewModel departmentViewModel)
    {
        Department? department = await this._context.Departments.FindAsync(departmentViewModel.Id);
        if (department is null)
        {
            return await Task.Run(() => NotFound());
        }

        department.Name = departmentViewModel.Name;
        department.Code = departmentViewModel.Code;

        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var department = await this._context.Departments.FindAsync(id);
        if (department is null)
        {
            return await Task.Run(() => NotFound());
        }

        this._context.Departments.Remove(department);
        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }
}
