using log4net;
using Sync.Api.CustomAttr;
using Sync.Api.Models;
using Sync.Api.Services;
using Sync.Core.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sync.Api.Controllers
{
    [CustomModule(Module = "PA")]
    public class PA_SyncImmiController : BaseController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static PAService pAService;
        public PA_SyncImmiController()
        {
            pAService = new PAService();
        }
        public async Task<IHttpActionResult> NhanLichSuXNCTuTTDH(List<ImmiHistory> transaction)
        {
            if (transaction != null)
            {
                var rs = await pAService.SaveXmlImmiHistory(transaction);
                return Ok(rs);
            }

            return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = "Không có bản ghi nào" });
        }
    }
}
