using ApiMicrosservicesDiscount.DTOs;

namespace ApiMicrosservicesDiscount.Repositories;


public interface ICouponRepository
{
    Task<CouponDTO> GetCouponByCode(string couponCode);
}
