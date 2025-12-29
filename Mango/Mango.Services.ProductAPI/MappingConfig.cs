using AutoMapper;
using Mango.Services.ProductAPI.Models.DTO;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.DTOs;

namespace Mango.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDTOs, Product>().ReverseMap();
                //cfg.CreateMap<Product, ProductDTOs>();
            });
            return mappingConfig;
        }
    }
}
