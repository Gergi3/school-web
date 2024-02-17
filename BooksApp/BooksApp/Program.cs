using BooksApp.Extensions;
using BooksApp.Infrastructure.Data;
using BooksApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionStringIdentifier = "BooksAppConnectionString";
var connectionString = builder.Configuration.GetConnectionString(connectionStringIdentifier)
	?? throw new InvalidOperationException($"Connection string '{connectionStringIdentifier}' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options
	=> options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
	// Reduce password params for development experience
	options.Password = new()
	{
		RequireDigit = false,
		RequiredLength = 6,
		RequireLowercase = false,
		RequireUppercase = false,
		RequiredUniqueChars = 0,
		RequireNonAlphanumeric = false
	};

	options.SignIn.RequireConfirmedAccount = false;
})
	.AddRoles<IdentityRole<Guid>>()
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Books}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
