using Mango.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Shared.DTO
{
    public class RequestDTO
    {
        public APIType apiType { get; set; }
        public string URL { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
