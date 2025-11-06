using static Mango.Web.Utility.SD;

namespace Mango.Web.Models
{
    public class RequestEDTO
    {
        public APIType APITYype { get; set; } = APIType.GET;
        public string URL { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
