using Sync.Core.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sync.Api.Controllers
{
    public class BaseController : ApiController
    {
        protected static string ConnectionStr = EppMaHoa.CommonManager.DecryptSHA256(ConfigurationSettings.AppSettings.Get("ConnectionString"));//MaHoa_GiaiMa.Decrypt(ConfigurationSettings.AppSettings.Get("ConnectionString"));
        protected DatabaseOracle databaseOracle = new DatabaseOracle(ConnectionStr);
        protected CallStoreCommon callStoreCommon = new CallStoreCommon(ConnectionStr);
        protected string accessToken = "";    
        protected string maTrungTam = ConfigurationSettings.AppSettings.Get("MaTrungTam");
    }
}
