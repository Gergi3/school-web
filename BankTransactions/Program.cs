using BankTransactionsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BankTransactionsApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllersWithViews();

        // Add context to the container
        builder.Services.AddDbContext<BankTransactionsAppContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("BankTransactionsApp"))
        );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for passbookion scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Passbooks}/{action=Index}/{id?}");

        app.Run();
    }
}
