using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> AssignRoleAsync(RegisterationRequestDTO registerationRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.POST,
                Data = registerationRequestDTO,
                URL = SD.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.POST,
                Data = loginRequestDTO,
                URL = SD.AuthAPIBase + "/api/auth/login"
            });
        }

        public async Task<ResponseDTO?> RegisterAsync(RegisterationRequestDTO registerationRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APITYype = SD.APIType.POST,
                Data = registerationRequestDTO,
                URL = SD.AuthAPIBase + "/api/auth/register"
            });
        }
    }
}
