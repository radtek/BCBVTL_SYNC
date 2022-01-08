using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Sync.Api.CustomAttr;
using Sync.Api.Models;
using Sync.Core.Entities.PA;
using Sync.Core.Helper;
using Sync.Core.Models;
using Sync.Core.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sync.Api.Controllers
{
    [CustomModule(Module = "PA")]
    public class PA_SyncXMNSController : BaseController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async Task<IHttpActionResult> NhanHoSoXMNSTuTTDH(XmnDocumentListX model)
        {
            try
            {
                if (model == null || model.xmnDocListA == null || string.IsNullOrEmpty(model.xmnDocListA.code))
                {
                    throw new Exception("Thiếu dữ liệu đầu vào!");
                }
                var xmlData = Common.ConvertObjectToXML(model);
                log.Info(DateTime.Now.ToString("yy.MM.dd HH:mm:ss") + "Get Lấy ds hồ sơ XMNS - " + model.xmnDocListA.code);
                log.Info(xmlData);

                List<OracleParameter> lstParam = new List<OracleParameter>
                {
                    new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input),
                    new OracleParameter("idQueue", OracleDbType.Decimal, model.idQueue, ParameterDirection.Input),
                    new OracleParameter("pMsg", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output)
                };
                var resultCreate = new Create_UpdateResponse();
                try
                {
                    var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.prc_insert_hs_xmns", lstParam);
                    var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                    if (resultProc == -1 && p_ERR_CODE == "OK")
                    {
                        resultCreate.success = true;
                        resultCreate.message = "Thêm mới ds hồ sơ xmns thành công!";
                        var resultLog = await callStoreCommon.CreateLogSync(model.xmnDocListA.code, "XMNS_A_LIST", JsonConvert.SerializeObject(resultCreate), resultCreate.message, " SUCCESS");
                        return Ok(new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công" });
                    }
                    else
                    {
                        resultCreate.success = false;
                        resultCreate.message = "Thêm mới ds hồ sơ xmns không thành công!. Lỗi: " + p_ERR_CODE;
                        var resultLog = await callStoreCommon.CreateLogSync(model.xmnDocListA.code, "XMNS_A_LIST", JsonConvert.SerializeObject(resultCreate), resultCreate.message, " FAIL");
                        return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = resultCreate.message });
                    }
                }
                catch (Exception ex)
                {
                    resultCreate.ConnectDB = databaseOracle.CheckConnect();
                    resultCreate.success = false;
                    resultCreate.message = "Thêm mới ds hồ sơ xmns không thành công!. Lỗi: " + ex.Message;

                    var resultLog = await callStoreCommon.CreateLogSync(model.xmnDocListA.code, "XMNS_A_LIST", JsonConvert.SerializeObject(resultCreate), resultCreate.message, "FAIL");
                    log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                    return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
                }

            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }
    }
}
