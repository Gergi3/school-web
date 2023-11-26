using ProductApp.Data;
using ProductApp.Models.Domain;
using ProductApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductApp.Controllers;

public class ProductsController : Controller
{
    private readonly ProductAppContext _context;

    public ProductsController(ProductAppContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Product> products = await this._context.Products.ToListAsync();

        HashSet<IndexProductViewModel> productsViewModel = products
            .Select(x => new IndexProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity,
                Category = x.Category
            })
            .ToHashSet();

        return await Task.Run(() => View(productsViewModel));
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
            Category = productViewModel.Category,
            Price = productViewModel.Price,
            Quantity = productViewModel.Quantity,
            Name = productViewModel.Name
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
            Category = product.Category,
            Name = product.Name,
            Quantity = product.Quantity,
            Price = product.Price
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

        product.Category = productViewModel.Category;
        product.Name = productViewModel.Name;
        product.Quantity = productViewModel.Quantity;
        product.Price = productViewModel.Price;

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
