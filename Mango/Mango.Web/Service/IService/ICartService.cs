using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDTO?> GetCartByUserIDAsync(string userID);
        Task<ResponseDTO?> UpsertCartAsync(CartDTOs cartDTOs);
        Task<ResponseDTO?> RemoveFromCartAsync(int cartDetailsID);
        Task<ResponseDTO?> ApplyCouponAsync(CartDTOs cartDTOs);
    }
}
