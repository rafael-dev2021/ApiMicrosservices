using System.Text.Json.Serialization;

namespace ApiMicrosservicesProduct.Models;
public sealed class Product(int id, string name, List<string> images, string description, decimal price, int stock, int categoryId)
{
    public int Id { get; set; } = id;
    public string Name { get; private set; } = name;
    public List<string> Images { get; private set; } = images;
    public string Description { get; private set; } = description;
    public decimal Price { get; private set; } = price;
    public int Stock { get; private set; } = stock;

    [JsonIgnore]
    public Category Category { get; private set; }
    public int CategoryId { get; private set; } = categoryId;
}