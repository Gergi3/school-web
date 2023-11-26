namespace BankTransactionsApp.Models.ViewModels;

public class CreatePassbookViewModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid DepartmentId { get; set; }
}
