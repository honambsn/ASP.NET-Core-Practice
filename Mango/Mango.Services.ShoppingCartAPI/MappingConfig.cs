using AutoMapper;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTOs;

namespace Mango.Services.ShoppingCartAPI
{
    //public class MappingConfig
    public class MappingConfig : Profile
    {
        //public static MapperConfiguration RegisterMaps()
        //{
        //    var mappingConfig = new MapperConfiguration(cfg =>
        //    {
        //        //cfg.CreateMap<ProductDTOs, Product>().ReverseMap();
        //        cfg.CreateMap<CartHeader, CartHeaderDTOs>().ReverseMap();
        //        cfg.CreateMap<CartDetails, CartDetailsDTOs>().ReverseMap();
        //        //cfg.CreateMap<Product, ProductDTOs>();
        //    });
        //    return mappingConfig;
        //}
        public MappingConfig()
        {
            CreateMap<CartHeader, CartHeaderDTOs>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDTOs>().ReverseMap();
        }
    }
}
