using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;
        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> ApplyCouponAsync(CartDTOs cartDTOs)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.POST,
                Data = cartDTOs,
                URL = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon",
            });
        }

        public async Task<ResponseDTO?> GetCartByUserIDAsync(string userID)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.GET,
                URL = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userID
            });
        }

        public async Task<ResponseDTO?> RemoveFromCartAsync(int cartDetailsID)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.POST,
                Data = cartDetailsID,
                URL = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart",
            });
        }


        public async Task<ResponseDTO?> UpsertCartAsync(CartDTOs cartDTOs)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.POST,
                Data = cartDTOs,
                URL = SD.ShoppingCartAPIBase + "/api/cart/CartUpsert",
            });
        }
    }
}
