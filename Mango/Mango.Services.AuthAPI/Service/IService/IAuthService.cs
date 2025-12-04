using Mango.Services.AuthAPI.Models.DTOs;

namespace Mango.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        //Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
        Task<string> Register(RegisterationRequestDTO registerationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<bool> AssignRole(string email, string roleName);
        //Task AssignRole(RegisterationRequestDTO model);
    }
}
