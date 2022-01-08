using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Sync.Api.CustomAttr;
using Sync.Api.Models.SyncCategory;
using Sync.Core.Helper;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sync.Api.Controllers
{
    [CustomModule(Module = "A")]
    public class A_SyncMasterController : BaseController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async Task<IHttpActionResult> NhanThongTinDanhMucTuTTDH(ListDatas model)
        {
            try
            {
                if (model == null)
                {
                    throw new Exception("Thiếu dữ liệu đầu vào!");
                }
                var xmlData = Common.ConvertObjectToXML(model);
                log.Info("XML data");
                log.Info(xmlData);

                List<OracleParameter> lstParam = new List<OracleParameter>
                {
                    new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input),
                    new OracleParameter("pMsg", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output)
                };
                var resultCreate = new Create_UpdateResponse();
                try
                {
                    var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.prc_cap_nhat_danh_muc", lstParam);
                    var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                    if (resultProc == -1 && p_ERR_CODE == "OK")
                    {
                        resultCreate.success = true;
                        resultCreate.message = "Thành công!";
                        var resultLog = await callStoreCommon.CreateLogSync("", "STATUS_A_LIST", JsonConvert.SerializeObject(resultCreate), resultCreate.message, " SUCCESS");
                        return Ok(new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công" });
                    }
                    else
                    {
                        resultCreate.success = false;
                        resultCreate.message = "Lỗi cập nhật trạng thái gửi danh sách A lên TTĐH: " + p_ERR_CODE;

                        var resultLog = await callStoreCommon.CreateLogSync("", "STATUS_A_LIST", JsonConvert.SerializeObject(resultCreate), resultCreate.message, " FAIL");
                        return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = p_ERR_CODE });
                    }
                }
                catch (Exception ex)
                {
                    resultCreate.ConnectDB = databaseOracle.CheckConnect();
                    resultCreate.success = false;
                    resultCreate.message = "Lỗi cập nhật trạng thái gửi danh sách A lên TTĐH: " + ex.Message;

                    var resultLog = await callStoreCommon.CreateLogSync("", "STATUS_A_LIST", JsonConvert.SerializeObject(resultCreate), resultCreate.message, "FAIL");
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
