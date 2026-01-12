using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Models.DTOs;
using Mango.Services.ShoppingCartAPI.Services.IService;
using Newtonsoft.Json;
using System.Text.Json.Serialization;


namespace Mango.Services.ShoppingCartAPI.Services
{
    public class ProductSerivce : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductSerivce(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<IEnumerable<ProductDTOs>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDTOs>>(Convert.ToString(resp.Result));
            }

            return new List<ProductDTOs>();
        }
    }
}
