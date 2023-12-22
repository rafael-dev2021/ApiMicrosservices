namespace ApiMicrosservicesProduct.Models;
public sealed class Category(int id, string name, string image)
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Image { get; private set; } = image;
    public ICollection<Product> Products { get; } = [];
}