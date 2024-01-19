using GiftsApp.Data;
using GiftsApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GiftsApp;

public class Program
{
	public static void Main(string[] args)
	{
		// admin@gmail.com

		var builder = WebApplication.CreateBuilder(args);

		var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		builder.Services.AddCors(options =>
		{
			options.AddPolicy(name: MyAllowSpecificOrigins,
							  policy =>
							  {
								  policy.WithOrigins("https://emoji-api.com/emojis")
									  .AllowAnyHeader()
									  .AllowAnyOrigin()
									  .AllowAnyMethod();
							  });
		});

		// Add services to the container.
		var connectionString = builder.Configuration.GetConnectionString("GiftsAppConnectionString")
			?? throw new InvalidOperationException("Connection string 'GiftsAppConnectionString' not found.");
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));
		builder.Services.AddDatabaseDeveloperPageExceptionFilter();

		builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
			.AddRoles<IdentityRole<Guid>>()
			.AddEntityFrameworkStores<ApplicationDbContext>();
		builder.Services.AddControllersWithViews();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseMigrationsEndPoint();
		}
		else
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseCors(MyAllowSpecificOrigins);

		app.UseAuthorization();
		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");
		app.MapRazorPages();

		app.Run();
	}
}
