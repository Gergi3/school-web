using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagerApp.Models.Domain;

public class Department
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required, StringLength(2)]
    public string Code { get; set; } = null!;

    [InverseProperty("Department")]
    public HashSet<Employee> Employees { get; set; } = new HashSet<Employee>();
}
