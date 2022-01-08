using Sync.Core.Helper;
using Sync.Web.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sync.Web.Controllers
{
    public class BaseController : Controller
    {
        protected static string ConnectionStr = EppMaHoa.CommonManager.DecryptSHA256(ConfigurationManager.AppSettings.Get("ConnectionString"));//MaHoa_GiaiMa.Decrypt(ConfigurationManager.AppSettings.Get("ConnectionString"));

        protected string maTrungTam = ConfigurationManager.AppSettings.Get("MaTrungTam");

        protected DatabaseOracle databaseOracle = new DatabaseOracle(ConnectionStr);
        protected CallStoreCommon callStoreCommon = new CallStoreCommon(ConnectionStr);

        protected ApiBase apiBase = new ApiBase();



    }
}