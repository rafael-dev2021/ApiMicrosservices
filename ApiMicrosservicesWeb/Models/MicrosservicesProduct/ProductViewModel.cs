using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMicrosservicesWeb.Models.MicrosservicesProduct;
public class ProductViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Maximum 100 characters and minimum 3 characters.")]
    [DisplayName("Name")]
    public string Name { get; set; } = string.Empty;
    public List<string> Images { get; set; } = [];

    [Required(ErrorMessage = "Description is required")]
    [StringLength(10000, MinimumLength = 10, ErrorMessage = "Maximum 10000 and minimum 10 characters.")]
    [DisplayName("Description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    [DisplayFormat(DataFormatString = "{0:C}")]
    [Range(1, 9999)]
    [DisplayName("Price")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Stock is required")]
    [Range(1, 9999)]
    [DisplayName("Stock")]
    public int Stock { get; set; }
    public string CategoryName { get; set; }

    [DisplayName("Categories")]
    public int CategoryId { get; set; }
}
