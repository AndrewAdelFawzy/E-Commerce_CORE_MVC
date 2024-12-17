using AutoMapper;
using Task.Core.Entities;
using Task.Web.Models;

namespace Task.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductViewModel>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));

            CreateMap<Brand, BrandViewModel>();            

        }
    }
}
