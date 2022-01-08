using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Core.Models
{
    public class ApiResult
    {
        public string code { get; set; }
        public string message { get; set; }
    }
    public class Response<T> : ApiResult
    {
        public T data { get; set; }
    }

    public class ResponseList<T> : ApiResult
    {
        public List<T> data { get; set; }
    }

    public class ApiResultUpdate : ApiResult
    {
        public string key { get; set; }
    }
}
