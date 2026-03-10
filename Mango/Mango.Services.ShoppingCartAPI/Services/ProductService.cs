using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Models.DTOs;
using Mango.Services.ShoppingCartAPI.Services.IService;
using Mango.Shared.DTO;
using Mango.Shared.Common;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Mango.Services.ShoppingCartAPI.Utility;
using ResponseDTO = Mango.Shared.DTO.ResponseDTO;


namespace Mango.Services.ShoppingCartAPI.Services
{
    public class ProductService : IProductService
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public ProductService(IHttpClientFactory clientFactory)
        //{
        //    _httpClientFactory = clientFactory;
        //}

        private readonly IBaseService _baseService;
        private readonly HttpClient _httpClient;


        public ProductService(HttpClient httpClient, IBaseService baseService)
        {
            _httpClient = httpClient;
            _baseService = baseService;
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


        //public async Task<IEnumerable<ProductDTOs>> GetProductsByIds(IEnumerable<int> productIds)
        //{
        //    var response = await _baseService.SendAsync(new RequestDTO
        //    {
        //        apiType = APIType.POST,
        //        URL = $"{SD.ProductAPIBase}/api/product/GetProductsByIds",
        //        Data = productIds
        //    });

        //    if (response == null || !response.IsSuccess || response.Result == null)
        //    {
        //        Console.WriteLine("test product services: ", response);
        //        return new List<ProductDTOs>();
        //    }

        //    Console.WriteLine("test product services: ", response.Result);

        //    return JsonConvert.DeserializeObject<IEnumerable<ProductDTOs>>(
        //        JsonConvert.SerializeObject(response.Result)
        //    ) ?? new List<ProductDTOs>();
        //}

        public async Task<IEnumerable<ProductDTOs>> GetProductsByIds(IEnumerable<int> productIds)
        {
            Console.WriteLine($"ProductService - ProductIDs: {string.Join(", ", productIds)}");
            Console.WriteLine($"ProductService - URL: {SD.ProductAPIBase}/api/ProductAPI/GetProductsByIds");

            var response = await _baseService.SendAsync(new RequestDTO
            {
                apiType = APIType.POST,
                URL = $"{SD.ProductAPIBase}/api/ProductAPI/GetProductsByIds",
                Data = productIds
            });

            Console.WriteLine($"ProductService - Response IsSuccess: {response?.IsSuccess}");
            Console.WriteLine($"ProductService - Response Result: {response?.Result}");
            Console.WriteLine($"ProductService - Response Message: {response?.Message}");

            if (response == null || !response.IsSuccess || response.Result == null)
            {
                Console.WriteLine("ProductService - Returning empty list");
                return new List<ProductDTOs>();
            }

            var resultJson = JsonConvert.SerializeObject(response.Result);
            Console.WriteLine($"ProductService - Result JSON: {resultJson}");

            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTOs>>(resultJson);

            Console.WriteLine($"ProductService - Deserialized count: {products?.Count() ?? 0}");

            return products ?? new List<ProductDTOs>();
        }
    }
}
