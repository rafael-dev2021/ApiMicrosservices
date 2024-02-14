namespace ApiMicrosservicesWeb.Models.MicrosservicesShoppingCart;

public class CouponViewModel
{
    public long Id { get; set; }
    public string? CouponCode { get; set; }
    public decimal Discount { get; set; }
}
