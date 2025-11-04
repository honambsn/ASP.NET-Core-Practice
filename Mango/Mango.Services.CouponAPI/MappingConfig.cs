using AutoMapper;
using Mango.Services.CouponAPI.Models.DTO;
using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CouponDTO, Coupon>();
                cfg.CreateMap<Coupon, CouponDTO>();
            });
            return mappingConfig;
        }
    }
}
