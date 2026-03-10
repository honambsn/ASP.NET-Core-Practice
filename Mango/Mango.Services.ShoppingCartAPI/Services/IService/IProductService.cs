using Mango.Services.ShoppingCartAPI.Models.DTOs;

namespace Mango.Services.ShoppingCartAPI.Services.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTOs>> GetProducts();
        Task<IEnumerable<ProductDTOs>> GetProductsByIds(IEnumerable<int> productIds);

    }
}
