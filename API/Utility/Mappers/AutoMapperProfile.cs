

using AutoMapper;

namespace API.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Core.Entities.Product, API.DTO.ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name));
            CreateMap<Core.Util.PagedProductList, API.DTO.PagedProductsDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}