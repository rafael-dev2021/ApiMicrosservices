using ApiMicrosservicesDiscount.Models;
using AutoMapper;

namespace ApiMicrosservicesDiscount.DTOs.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CouponDTO, Coupon>().ReverseMap();
    }
}
