using BankTransactionsApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankTransactionsApp.Data;

public class BankTransactionsAppContext : DbContext
{
    public BankTransactionsAppContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Passbook> Passbooks { get; set; } = null!;
}
