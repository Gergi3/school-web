using ProductApp.Enums;

namespace ProductApp.Models.ViewModels;

public class IndexProductViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Categories Category { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public string Code
        => $"{this.Name.Substring(0, 2)}{this.Category.ToString().Substring(0, 3)}{this.Id.ToString().Substring(0, 3)}";
}
