namespace CompanyManagerApp.Models.ViewModels;

public class EditDepartmentViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
