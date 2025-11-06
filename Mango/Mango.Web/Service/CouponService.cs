using Mango.Web.Models;
using Mango.Web.Service.IService;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public Task<ResponseDTO?> CreateCouponsAsync(CouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> DeleteCouponsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> GetAllCouponsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> GetCouponAsync(string couponCode)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> GetCouponByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> UpdateCouponsAsync(CouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }
    }
}
