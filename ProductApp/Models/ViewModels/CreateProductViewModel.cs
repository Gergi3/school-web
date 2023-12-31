using ProductApp.Enums;

namespace ProductApp.Models.ViewModels;

public class CreateProductViewModel
{
    public string Name { get; set; } = null!;
    public Categories Category { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
