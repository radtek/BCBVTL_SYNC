using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Sync.Api.CustomAttr;
using Sync.Api.Models;
using Sync.Api.Services;
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
    [CustomModule(Module = "PA")]
    public class PA_SyncTranController : BaseController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private PAService pAService;
        public PA_SyncTranController()
        {
            pAService = new PAService();
        }

        public async Task<IHttpActionResult> NhanDanhSachCTuTTDH(List<HandoverC> model)
        {
            try
            {
                var rs = await pAService.SaveDSCTuTTDH(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }

        public async Task<IHttpActionResult> NhanDanhSachBTuTTDH(ListBModel model)
        {
            try
            {
                var listRs = new List<HandoverResultModel>();
                if (model != null && !string.IsNullOrEmpty(model.packageId))
                {
                    var rs = await pAService.SaveDSBTuTTDH(model);
                    return Ok(rs);
                }
                throw new Exception("Danh sách rỗng hoặc sai định dạng.");
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }

        public async Task<IHttpActionResult> NhanKetQuaGuiDSALenTTDH(List<HandoverAStatus> model)
        {
            try
            {
                if (model == null || model.Count == 0)
                {
                    throw new Exception("Thiếu dữ liệu đầu vào!");
                }

                var listBs = new HandoverAStatuss { ListA = model };

                var xmlData = Common.ConvertObjectToXML(listBs);
                List<OracleParameter> lstParam = new List<OracleParameter>
                {
                    new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input),
                    new OracleParameter("pMsg", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output)
                };
                var resultCreate = new Create_UpdateResponse();
                try
                {
                    var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.prc_update_status_send_list_a_v5", lstParam);
                    var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                    if (resultProc == -1 && p_ERR_CODE == "OK")
                    {
                        resultCreate.success = true;
                        resultCreate.message = "";
                        var resultLog = await callStoreCommon.CreateLogSync(model[0].packageId, "STATUS_A_LIST", JsonConvert.SerializeObject(resultCreate), resultCreate.message, "SUCCESS");
                        return Ok(new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công" });
                    }
                    else
                    {
                        resultCreate.success = false;
                        resultCreate.message = "Lỗi cập nhật trạng thái gửi danh sách A lên TTĐH: " + p_ERR_CODE;
                        var resultLog = await callStoreCommon.CreateLogSync(model[0].packageId, "STATUS_A_LIST", JsonConvert.SerializeObject(resultCreate), resultCreate.message, "FAIL");
                        return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = p_ERR_CODE });
                    }
                }
                catch (Exception ex)
                {
                    resultCreate.ConnectDB = databaseOracle.CheckConnect();
                    resultCreate.success = false;
                    resultCreate.message = "Lỗi cập nhật trạng thái gửi danh sách A lên TTĐH: " + ex.Message;

                    var resultLog = await callStoreCommon.CreateLogSync(model[0].packageId, "STATUS_A_LIST", JsonConvert.SerializeObject(resultCreate), resultCreate.message, "FAIL");
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

        public async Task<IHttpActionResult> NhanThongTinHoSoDayDuTuTTDH(TransactionFull model)
        {
            try
            {
                if (model == null || model.transactionF == null || string.IsNullOrEmpty(model.transactionF.transactionId))
                {
                    throw new Exception("Thiếu dữ liệu đầu vào!");
                }
                var rs = await pAService.NhanThongTinHoSoDayDuTuTTDH(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }


        public async Task<IHttpActionResult> NhanThongTinHoChieuTuTTDH(PassportUpload model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.transactionId))
                {
                    throw new Exception("Thiếu dữ liệu đầu vào!");
                }
                var xmlData = Common.ConvertObjectToXML(model);
                log.Info("Start insert");
                log.Info("XML: ");
                log.Info(xmlData);

                List<OracleParameter> lstParam = new List<OracleParameter>
                {
                    new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input),
                    new OracleParameter("pMsg", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output)
                };
                var resultCreate = new Create_UpdateResponse();
                try
                {
                    var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.UPDATE_PASSPORT_FROM_NPC", lstParam);
                    var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                    if (resultProc == -1 && p_ERR_CODE == "OK")
                    {
                        resultCreate.success = true;
                        resultCreate.message = "Thành công!";
                        var resultLog = await callStoreCommon.CreateLogSync(model.passportNo, "PASSPORT", JsonConvert.SerializeObject(resultCreate), resultCreate.message, " SUCCESS");
                        return Ok(new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công" });
                    }
                    else
                    {
                        resultCreate.success = false;
                        resultCreate.message = "Cập nhật dữ liệu lỗi: " + p_ERR_CODE;
                        var resultLog = await callStoreCommon.CreateLogSync(model.passportNo, "PASSPORT", JsonConvert.SerializeObject(resultCreate), resultCreate.message, " FAIL");
                        return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = p_ERR_CODE });
                    }
                }
                catch (Exception ex)
                {
                    resultCreate.ConnectDB = databaseOracle.CheckConnect();
                    resultCreate.success = false;
                    resultCreate.message = "Cập nhật dữ liệu lỗi: " + ex.Message;

                    var resultLog = await callStoreCommon.CreateLogSync(model.passportNo, "PASSPORT", JsonConvert.SerializeObject(resultCreate), resultCreate.message, " FAIL");
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
