using BankTransactionsApp.Data;
using BankTransactionsApp.Models.Domain;
using BankTransactionsApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankTransactionsApp.Controllers;

public class PassbooksController : Controller
{
    private readonly BankTransactionsAppContext _context;

    public PassbooksController(BankTransactionsAppContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Passbook> passbooks = await this._context.Passbooks.ToListAsync();

        HashSet<IndexPassbookViewModel> passbooksViewModel = passbooks
            .Select(x => new IndexPassbookViewModel()
            {
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                Id = x.Id,
                Name = x.Name,
                Salary = x.Salary,
            })
            .ToHashSet();

        return await Task.Run(() => View(passbooksViewModel));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePassbookViewModel passbookViewModel)
    {
        Passbook passbook = new Passbook()
        {
            Id = new Guid(),
            Name = passbookViewModel.Name,
            Email = passbookViewModel.Email,
            Salary = passbookViewModel.Salary,
            DateOfBirth = passbookViewModel.DateOfBirth,
        };

        await this._context.Passbooks.AddAsync(passbook);
        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        Passbook? passbook = await this._context.Passbooks.FindAsync(id);

        if (passbook is null)
        {
            return await Task.Run(() => NotFound());
        }

        EditPassbookViewModel passbookViewModel = new EditPassbookViewModel()
        {
            Id = passbook.Id,
            Email = passbook.Email,
            Salary = passbook.Salary,
            DateOfBirth = passbook.DateOfBirth,
            Name = passbook.Name
        };

        return await Task.Run(() => View(passbookViewModel));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPassbookViewModel passbookViewModel)
    {
        Passbook? passbook = await this._context.Passbooks.FindAsync(passbookViewModel.Id);

        if (passbook is null)
        {
            return await Task.Run(() => NotFound());
        }

        passbook.Email = passbookViewModel.Email;
        passbook.Salary = passbookViewModel.Salary;
        passbook.DateOfBirth = passbookViewModel.DateOfBirth;
        passbook.Name = passbookViewModel.Name;

        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var passbook = await this._context.Passbooks.FindAsync(id);
        if (passbook is null)
        {
            return await Task.Run(() => NotFound());
        }

        this._context.Passbooks.Remove(passbook);
        await this._context.SaveChangesAsync();

        return await Task.Run(() => this.RedirectToAction("Index"));
    }
}
