using Mango.Services.ShoppingCartAPI.Services.IService;
using Mango.Shared.Common;
using Mango.Shared.DTO;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Services.ShoppingCartAPI.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseService(
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var message = new HttpRequestMessage();

                message.Headers.Add("Accept", "application/json");

                if (withBearer)
                {
                    var token = _httpContextAccessor
                        .HttpContext?
                        .Request
                        .Headers["Authorization"]
                        .FirstOrDefault();

                    if (!string.IsNullOrEmpty(token))
                    {
                        message.Headers.Add("Authorization", token);
                    }

                    Console.WriteLine("show my TOKEN: " + token);
                }

                message.RequestUri = new Uri(requestDTO.URL);

                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(requestDTO.Data),
                        Encoding.UTF8,
                        "application/json");
                }

                message.Method = requestDTO.apiType switch
                {
                    APIType.POST => HttpMethod.Post,
                    APIType.PUT => HttpMethod.Put,
                    APIType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                var response = await client.SendAsync(message);

                var apiContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}

