using AutoMapper;
using Platzi_Dotnet.Models;
using Platzi_Dotnet.VIewModels;

namespace Platzi_Dotnet.Migrations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
