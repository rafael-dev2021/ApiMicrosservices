namespace ApiMicrosservicesProduct.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public List<string> Images { get; set; } 
    public string CategoryName { get; set; }
    public int CategoryId { get; set; }
}
