using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApiMicrosservicesWeb.Models.MicrosservicesProduct;
public class CategoryViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Maximum 50 characters and minimum 3 characters.")]
    [DisplayName("Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Image is required")]
    [StringLength(600, ErrorMessage = "Maximum 600 characters.")]
    [DisplayName("Image")]
    public string Image { get; set; } = string.Empty;
}
