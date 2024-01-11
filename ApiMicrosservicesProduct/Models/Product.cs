namespace ApiMicrosservicesProduct.Models;

public class Product
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    public int Stock { get; protected set; }
    public decimal Price { get; protected set; }
    public List<string> Images { get; protected set; } = [];
    public byte[] RowVersion { get; protected set; }
    public Category Category { get; protected set; }
    public int CategoryId { get; protected set; }

    public Product() { }
    public Product(int id, string name, string description, int stock, decimal price, List<string> images, int categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        Stock = stock;
        Price = price;
        Images = images;
        CategoryId = categoryId;
    }
}
