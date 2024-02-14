using ApiMicrosservicesDiscount.Context;
using ApiMicrosservicesDiscount.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiMicrosservicesDiscount.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly AppDbContext _context;
    private IMapper _mapper;

    public CouponRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CouponDTO> GetCouponByCode(string couponCode)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c =>
                          c.CouponCode == couponCode);

        return _mapper.Map<CouponDTO>(coupon);
    }
}
