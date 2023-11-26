using ProductApp.Data;
using ProductApp.Models.Domain;
using ProductApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductApp.Controllers;

public class ProductController : Controller
{
    private readonly ProductAppContext _context;

    public ProductController(ProductAppContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? department = null)
    {
        List<Product> products = await this._context.Products.ToListAsync();

        HashSet<IndexProductViewModel> productsViewModel = products
            .Select(x => new IndexProductViewModel()
            {
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                Id = x.Id,
                Name = x.Name,
                Salary = x.Salary,
            })
            .ToHashSet();

        return await Task.Run(() => View(productsViewModel));
    }

    [HttpGet]
    public async Task<IActionResult> View(Guid id)
    {
        var product = await this._context.Products.FindAsync(id);

        if (product is null)
        {
            return await Task.Run(() => NotFound());
        }

        var productViewModel = new ViewProductViewModel()
        {
            DateOfBirth = product.DateOfBirth,
            Email = product.Email,
            Name = product.Name,
            Salary = product.Salary,
            Id = product.Id
        };

        return this.View(productViewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductViewModel productViewModel)
    {
        Product product = new Product()
        {
            Id = new Guid(),
            Name = productViewModel.Name,
            Email = productViewModel.Email,
            Salary = productViewModel.Salary,
            DateOfBirth = productViewModel.DateOfBirth,
        };

        await this._context.Products.AddAsync(product);
        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        Product? product = await this._context.Products.FindAsync(id);

        if (product is null)
        {
            return await Task.Run(() => NotFound());
        }

        EditProductViewModel productViewModel = new EditProductViewModel()
        {
            Id = product.Id,
            Email = product.Email,
            Salary = product.Salary,
            DateOfBirth = product.DateOfBirth,
            Name = product.Name
        };

        return await Task.Run(() => View(productViewModel));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProductViewModel productViewModel)
    {
        Product? product = await this._context.Products.FindAsync(productViewModel.Id);

        if (product is null)
        {
            return await Task.Run(() => NotFound());
        }

        product.Email = productViewModel.Email;
        product.Salary = productViewModel.Salary;
        product.DateOfBirth = productViewModel.DateOfBirth;
        product.Name = productViewModel.Name;

        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await this._context.Products.FindAsync(id);
        if (product is null)
        {
            return await Task.Run(() => NotFound());
        }

        this._context.Products.Remove(product);
        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }
}
