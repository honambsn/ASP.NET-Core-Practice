using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Services.IService;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.Services
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<CouponDTOs> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/CouponAPI/GetByCode/{couponCode}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var apiContent = await response.Content.ReadAsStringAsync();

            var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

            if (resp?.IsSuccess == true && resp.Result != null)
            {
                //return JsonConvert.DeserializeObject<CouponDTOs>(Convert.ToString(resp.Result));
                return JsonConvert.DeserializeObject<CouponDTOs>(JsonConvert.SerializeObject(resp.Result));
            }

            return new CouponDTOs();
        }
    }
}
