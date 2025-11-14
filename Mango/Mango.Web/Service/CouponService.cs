using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> CreateCouponsAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO() 
            { 
                APITYype = SD.APIType.POST, 
                Data = couponDTO,
                URL = SD.CouponAPIBase + "/api/CouponAPI",
            });
        }

        public async Task<ResponseDTO?> DeleteCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO() 
            { 
                APITYype = SD.APIType.DELETE, 
                URL = SD.CouponAPIBase + "/api/CouponAPI/" + id,
            });
        }

        public async Task<ResponseDTO?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.GET,
                URL = SD.CouponAPIBase + "/api/CouponAPI",
            });
        }

        public async Task<ResponseDTO?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.GET,
                URL = SD.CouponAPIBase + "/api/CouponAPI/GetByCode/" + couponCode,
            });

        }

        public async Task<ResponseDTO?> GetCouponByIDAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO() { 
                APITYype = SD.APIType.GET,
                URL = SD.CouponAPIBase + "/api/CouponAPI/" + id,
            });
        }

        public async Task<ResponseDTO?> UpdateCouponsAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.PUT,
                Data = couponDTO,
                URL = SD.CouponAPIBase + "/api/CouponAPI",
            });
        }
    }
}
