using CompanyManagerApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagerApp.Data;

public class CompanyManagerAppContext : DbContext
{
    public CompanyManagerAppContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
}
