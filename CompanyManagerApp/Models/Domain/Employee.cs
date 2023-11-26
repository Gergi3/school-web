using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagerApp.Models.Domain;

public class Employee
{
    [Key]
    public Guid Id { get; set; }

    [Required, MinLength(3), MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required, MinLength(6), RegularExpression(Constants.EmailRegex)]
    public string Email { get; set; } = null!;

    [Required, Column(TypeName = "money")]
    public decimal Salary { get; set; }

    [Required, Column(TypeName = "date")]
    public DateTime DateOfBirth { get; set; }

    [ForeignKey(nameof(Domain.Department))]
    public Guid DepartmentId { get; set; }
    public Domain.Department Department { get; set; } = null!;
}
