namespace ApiMicrosservicesShoppingCart.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public List<string> Images { get; set; } = [];

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }
    public string CategoryName { get; set; }
}
