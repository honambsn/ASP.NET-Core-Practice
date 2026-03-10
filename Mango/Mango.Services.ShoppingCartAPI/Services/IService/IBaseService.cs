using Mango.Shared.DTO;

namespace Mango.Services.ShoppingCartAPI.Services.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true);

    }
}
