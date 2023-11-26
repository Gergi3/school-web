using CompanyManagerApp.Data;
using CompanyManagerApp.Models.Domain;
using CompanyManagerApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
}
