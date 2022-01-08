using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Sync.Api.CustomAttr;
using Sync.Api.Models;
using Sync.Api.Services;
using Sync.Api.Utils;
using Sync.Core.Helper;
using Sync.Core.Models;
using Sync.Core.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Sync.Api.Controllers
{
    [CustomModule(Module = "Perso")]
    public class Perso_SyncTranController : BaseController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private PersoService persoService;
        public Perso_SyncTranController()
        {
            persoService = new PersoService();
        }
        public async Task<IHttpActionResult> NhanAnhSuaTuTTDH(List<ttImages> model)
        {
            try
            {
                var rs = await persoService.SaveAnhSuaTuTTDHAsync(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }


    }
}