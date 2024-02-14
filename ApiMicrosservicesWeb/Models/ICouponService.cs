using ApiMicrosservicesWeb.Models.MicrosservicesShoppingCart;

namespace ApiMicrosservicesWeb.Models;
public interface ICouponService
{
    Task<CouponViewModel> GetDiscountCoupon(string couponCode, string token);
}
