using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDTO?> GetProductAsync(string ProductCode);
        Task<ResponseDTO?> GetAllProductsAsync();
        Task<ResponseDTO?> GetProductByIDAsync(int id);
        Task<ResponseDTO?> CreateProductsAsync(ProductDTOs productDTO);
        Task<ResponseDTO?> UpdateProductsAsync(ProductDTOs productDTO);
        Task<ResponseDTO?> DeleteProductsAsync(int id);
    }
}
