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
    [CustomModule(Module = "PA")]
    public class PA_InvestigationController : BaseController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static PAService pAService;
        public PA_InvestigationController()
        {
            pAService = new PAService();
        }
        public async Task<IHttpActionResult> NhanKetQuaNghiTrung(ResponseDowloadBuff model)
        {
            try
            {
                if (model != null)
                {
                    var rs = await pAService.SaveXmlNhanThongTinNghiTrung(model);
                    return Ok(rs);
                }

                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = "Không có bản ghi nào" });
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }

        public async Task<IHttpActionResult> NhanThongTinChiTietCuaNghiTrung(ObjDetailBuffPerson model)
        {
            try
            {
                if (model != null)
                {
                    var rs = await pAService.SaveDetailBuf(model);
                    return Ok(rs);
                }
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = "Dữ liệu lấy từ TTDH rỗng." });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }
    }
}
