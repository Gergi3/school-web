using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductApp.Enums;

namespace ProductApp.Models.Domain;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required, MinLength(3), MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    public Categories Category { get; set; }

    [Required, Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }
}
