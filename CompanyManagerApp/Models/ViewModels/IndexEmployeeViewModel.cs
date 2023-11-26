namespace CompanyManagerApp.Models.ViewModels;

public class IndexEmployeeViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string DepartmentName { get; set; } = null!;
}
