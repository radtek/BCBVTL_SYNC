using Sync.Api.Models;
using Sync.Api.Services;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Sync.Api.CustomAttr;

namespace Sync.Api.Controllers
{
    [CustomModule(Module = "Perso")]
    public class Perso_GetPassportReturnController : BaseController
    {
        private PersoService persoService;
        // GET: Perso_GetPassportReturn
        public Perso_GetPassportReturnController()
        {
            persoService = new PersoService();
        }

        public async Task<IHttpActionResult> NhanHoChieuTraVe(List<PPReturnData> model)
        {
            try
            {
                var rs = await persoService.GetPassPortReturn(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }
    }
}