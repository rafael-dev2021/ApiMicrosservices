using ApiMicrosservicesProduct.Models;
using AutoMapper;

namespace ApiMicrosservicesProduct.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<Product, ProductDto>()
            .ForMember(x => x.CategoryName, m => m.MapFrom(x => x.Category.Name));
    }
}
