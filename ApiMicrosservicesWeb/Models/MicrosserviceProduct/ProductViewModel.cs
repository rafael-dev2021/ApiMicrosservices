namespace ApiMicrosservicesWeb.Models.MicrosserviceProduct;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public List<string> Images { get; set; } = [];
    public string CategoryName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}
