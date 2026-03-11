using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Services.IService;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Mango.Services.ShoppingCartAPI.Services
{
    public class CouponService : ICouponService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CouponService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CouponDTOs> GetCoupon(string couponCode)
        {
            // get token
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
            }

            //var client = _httpClientFactory.CreateClient("Coupon");
            var response = await _httpClient.GetAsync($"/api/CouponAPI/GetByCode/{couponCode}");

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
