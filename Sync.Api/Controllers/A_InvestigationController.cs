using log4net;
using Newtonsoft.Json;
using Sync.Api.CustomAttr;
using Sync.Api.Models;
using Sync.Api.Models.Buf;
using Sync.Api.Services;
using Sync.Api.Utils;
using Sync.Core.Helper;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sync.Api.Controllers
{
    [CustomModule(Module = "A")]
    public class A_InvestigationController : BaseController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static AService aService;
        public A_InvestigationController()
        {
            aService = new AService();
        }
        public async Task<IHttpActionResult> NhanKetQuaNghiTrung(ResponseDowloadBuff model)
        {
            if (model != null)
            {
                var rs = await aService.SaveXmlNhanThongTinNghiTrung(model);
                return Ok(rs);
            }

            return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = "Không có bản ghi nào" });
        }
    }
}
