using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Models.DTOs;
using Mango.Services.ShoppingCartAPI.Services.IService;
using Newtonsoft.Json;
using System.Text.Json.Serialization;


namespace Mango.Services.ShoppingCartAPI.Services
{
    public class ProductService : IProductService
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public ProductService(IHttpClientFactory clientFactory)
        //{
        //    _httpClientFactory = clientFactory;
        //}

        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //public async Task<IEnumerable<ProductDTOs>> GetProducts()
        //{
        //    var client = _httpClientFactory.CreateClient("Product");
        //    var response = await client.GetAsync($"/api/product");
        //    var apiContent = await response.Content.ReadAsStringAsync();
        //    var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

        //    if (resp.IsSuccess)
        //    {
        //        return JsonConvert.DeserializeObject<IEnumerable<ProductDTOs>>(Convert.ToString(resp.Result));
        //    }

        //    return new List<ProductDTOs>();
        //}

        public async Task<IEnumerable<ProductDTOs>> GetProducts()
        {
         //   var client = _httpClientFactory.CreateClient("Product");
            var response = await _httpClient.GetAsync("/api/product");

            
            if (!response.IsSuccessStatusCode)
            {
                return new List<ProductDTOs>();
            }

            var apiContent = await response.Content.ReadAsStringAsync();

            var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

            
            if (resp == null || !resp.IsSuccess || resp.Result == null)
            {
                return new List<ProductDTOs>();
            }

            return JsonConvert.DeserializeObject<IEnumerable<ProductDTOs>>(
                Convert.ToString(resp.Result)
            ) ?? new List<ProductDTOs>();
        }

    }
}
