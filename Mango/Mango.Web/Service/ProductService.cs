using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> CreateProductsAsync(ProductDTOs ProductDTO)
        {
            return await _baseService.SendAsync(new RequestDTO() 
            { 
                APITYype = SD.APIType.POST, 
                Data = ProductDTO,
                URL = SD.ProductAPIBase + "/api/ProductAPI",
            });
        }

        public async Task<ResponseDTO?> DeleteProductsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO() 
            { 
                APITYype = SD.APIType.DELETE, 
                URL = SD.ProductAPIBase + "/api/ProductAPI/" + id,
            });
        }

        public async Task<ResponseDTO?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.GET,
                URL = SD.ProductAPIBase + "/api/ProductAPI",
            });
        }

        public async Task<ResponseDTO?> GetProductAsync(string ProductCode)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.GET,
                URL = SD.ProductAPIBase + "/api/ProductAPI/GetByCode/" + ProductCode,
            });

        }

        public async Task<ResponseDTO?> GetProductByIDAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO() { 
                APITYype = SD.APIType.GET,
                URL = SD.ProductAPIBase + "/api/ProductAPI/" + id,
            });
        }

        public async Task<ResponseDTO?> UpdateProductsAsync(ProductDTOs ProductDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.PUT,
                Data = ProductDTO,
                URL = SD.ProductAPIBase + "/api/ProductAPI",
            });
        }
    }
}
