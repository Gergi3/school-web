using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApp.Models.Domain;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required, MinLength(3), MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required, MinLength(6)]
    public string Email { get; set; } = null!;

    [Required, Column(TypeName = "money")]
    public decimal Salary { get; set; }

    [Required, Column(TypeName = "date")]
    public DateTime DateOfBirth { get; set; }
}
