using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Core.Models
{
    public class TokenInfo : BaseRespone
    {
        public TokenResponse response { get; set; }
    }

    public class TokenResponse
    {
        public string token { get; set; }
    }

    public class GetTokenInfo
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class BaseRespone
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}
