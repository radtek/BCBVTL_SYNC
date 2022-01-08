using Sync.Core.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Sync.Api.Services
{
    public class BaseService
    {
        protected static string ConnectionStr = EppMaHoa.CommonManager.DecryptSHA256(ConfigurationSettings.AppSettings.Get("ConnectionString"));//MaHoa_GiaiMa.Decrypt(ConfigurationSettings.AppSettings.Get("ConnectionString"));
        protected string maTrungTam = ConfigurationSettings.AppSettings.Get("MaTrungTam");

        protected DatabaseOracle databaseOracle = new DatabaseOracle(ConnectionStr);
        protected CallStoreCommon callStoreCommon = new CallStoreCommon(ConnectionStr);

    }
}