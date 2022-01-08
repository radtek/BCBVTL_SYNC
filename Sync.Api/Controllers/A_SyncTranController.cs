using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Sync.Api.CustomAttr;
using Sync.Api.Models;
using Sync.Api.Services;
using Sync.Api.Utils;
using Sync.Core.Helper;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sync.Api.Controllers
{
    [CustomModule(Module = "A")]
    public class A_SyncTranController : BaseController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private AService aService;
        public A_SyncTranController()
        {
            aService = new AService();
        }
        public async Task<IHttpActionResult> NhanDanhSachCTuTTDH(List<HandoverC> model)
        {
            try
            {
                var rs = await aService.SaveDSCTuTTDH(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }
        public async Task<IHttpActionResult> NhanDanhSachATuTTDH(List<HandoverA> model)
        {
            try
            {
                var rs = await aService.SaveDSATuTTDH(model);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }

        public async Task<IHttpActionResult> NhanHoSoTuTTDH(Transaction requestData)
        {
            try
            {
                if (requestData == null)
                    throw new Exception("Thiếu dữ liệu đầu vào");

                log.Info("================= Lấy hồ sơ =================");
                log.Info(JsonConvert.SerializeObject(requestData));
                log.Info("================= Kết thúc lấy hồ sơ =================");

                if (requestData.documents != null && requestData.documents.Count > 0)
                {
                    requestData.personAtts = new List<PersonAttachment>();
                    var vanTays = requestData.documents.Where(x => x.docType != "SCAN_DOCUMENT" && x.docType != "SCAN_OTHER").ToList();
                    if (vanTays != null && vanTays.Count > 0)
                        requestData.personAtts.Add(ChuyenVanTayHoSo(vanTays));
                    requestData.documents = requestData.documents.Where(x => x.docType == "SCAN_DOCUMENT" || x.docType == "SCAN_OTHER").ToList();
                }
                string xmlData = Common.ConvertObjectToXML(requestData);

                PersonAServices personAService = new PersonAServices();

                var resultUpdate = await personAService.CreatePersonXml(xmlData, requestData.transactionId);

                // Ghi log xuống DB
                string jsonDB = JsonConvert.SerializeObject(resultUpdate, Formatting.Indented);

                var resultLog = await callStoreCommon.CreateLogSync(requestData.transactionId, "PERSON", jsonDB, resultUpdate.message, (resultUpdate.success == false ? "FAIL" : "SUCCESS"));

                if (resultUpdate.success)
                {
                    return Ok(new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công" });
                }
                else
                {
                    return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = resultUpdate.message });
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }

        public PersonAttachment ChuyenVanTayHoSo(List<TransactionDocument> transactionDocuments)
        {
            var result = new PersonAttachment();
            var fpPNG = new List<TransactionDocument>();
            foreach (var item in transactionDocuments)
            {
                if (item.docType == "PH_CAP")
                {
                    result.BASE64 = item.docData;
                    result.FILE_NAME = item.fileName;
                    result.createdBy = item.createdBy;
                    result.createdDate = item.createdDate;
                    result.createdByName = item.createdByName;
                    result.updatedBy = item.updatedBy;
                    result.updatedDate = item.updatedDate;
                    result.updatedByName = item.updatedByName;
                }

                if (item.docType == "FP_ROLLING_WSQ")
                {
                    var resultConvert = Convert_WSQ_To_Image.Convert_WSQ(item.docData);
                    if (resultConvert.success)
                    {
                        item.docData = resultConvert.code;
                        item.docType = "FP_PNG";
                        fpPNG.Add(item);
                    }

                    if (item.serialNo == "1" || item.serialNo == "01")
                    {
                        result.R_CAI_PHAI_WSQ = item.docData;
                        result.R_CAI_PHAI_PNG = !string.IsNullOrEmpty(resultConvert.code) ? resultConvert.code : "";
                    }

                    else if (item.serialNo == "2" || item.serialNo == "02")
                    {
                        result.R_TRO_PHAI_WSQ = item.docData;
                        result.R_TRO_PHAI_PNG = !string.IsNullOrEmpty(resultConvert.code) ? resultConvert.code : "";
                    }
                    else if (item.serialNo == "3" || item.serialNo == "03")
                    {
                        result.R_GIUA_PHAI_WSQ = item.docData;
                        result.R_GIUA_PHAI_PNG = !string.IsNullOrEmpty(resultConvert.code) ? resultConvert.code : "";
                    }
                    else if (item.serialNo == "4" || item.serialNo == "04")
                    {
                        result.R_NHAN_PHAI_WSQ = item.docData;
                        result.R_NHAN_PHAI_PNG = !string.IsNullOrEmpty(resultConvert.code) ? resultConvert.code : "";
                    }
                    else if (item.serialNo == "5" || item.serialNo == "05")
                    {
                        result.R_UT_PHAI_WSQ = item.docData;
                        result.R_UT_PHAI_PNG = !string.IsNullOrEmpty(resultConvert.code) ? resultConvert.code : "";
                    }
                    else if (item.serialNo == "6" || item.serialNo == "06")
                    {
                        result.R_CAI_TRAI_WSQ = item.docData;
                        result.R_CAI_TRAI_PNG = !string.IsNullOrEmpty(resultConvert.code) ? resultConvert.code : "";
                    }
                    else if (item.serialNo == "7" || item.serialNo == "07")
                    {
                        result.R_TRO_TRAI_WSQ = item.docData;
                        result.R_TRO_TRAI_PNG = !string.IsNullOrEmpty(resultConvert.code) ? resultConvert.code : "";
                    }
                    else if (item.serialNo == "8" || item.serialNo == "08")
                    {
                        result.R_GIUA_TRAI_WSQ = item.docData;
                        result.R_GIUA_TRAI_PNG = !string.IsNullOrEmpty(resultConvert.code) ? resultConvert.code : "";
                    }
                    else if (item.serialNo == "9" || item.serialNo == "09")
                    {
                        result.R_NHAN_TRAI_WSQ = item.docData;
                        result.R_NHAN_TRAI_PNG = !string.IsNullOrEmpty(resultConvert.code) ? resultConvert.code : "";
                    }
                    else if (item.serialNo == "10")
                    {
                        result.R_UT_TRAI_WSQ = item.docData;
                        result.R_UT_TRAI_PNG = !string.IsNullOrEmpty(resultConvert.code) ? resultConvert.code : "";
                    }
                }

                if (item.docType == "FP_MNU")
                {
                    if (item.serialNo == "1" || item.serialNo == "01")
                        result.R_CAI_PHAI_MNU = item.docData;

                    else if (item.serialNo == "2" || item.serialNo == "02")
                        result.F_TRO_PHAI_MNU = item.docData;
                    else if (item.serialNo == "3" || item.serialNo == "03")
                        result.F_GIUA_PHAI_MNU = item.docData;
                    else if (item.serialNo == "4" || item.serialNo == "04")
                        result.F_NHAN_PHAI_MNU = item.docData;
                    else if (item.serialNo == "5" || item.serialNo == "05")
                        result.F_UT_PHAI_MNU = item.docData;
                    else if (item.serialNo == "6" || item.serialNo == "06")
                        result.F_CAI_TRAI_MNU = item.docData;
                    else if (item.serialNo == "7" || item.serialNo == "07")
                        result.F_TRO_TRAI_MNU = item.docData;
                    else if (item.serialNo == "8" || item.serialNo == "08")
                        result.F_GIUA_TRAI_MNU = item.docData;
                    else if (item.serialNo == "9" || item.serialNo == "09")
                        result.F_NHAN_TRAI_MNU = item.docData;
                    else if (item.serialNo == "10")
                        result.F_UT_TRAI_MNU = item.docData;
                }

                if (item.docType == "FP_WSQ")
                {
                    if (item.serialNo == "1" || item.serialNo == "01")
                        result.F_CAI_PHAI_MNU = item.docData;
                    else if (item.serialNo == "2" || item.serialNo == "02")
                        result.F_TRO_PHAI_MNU = item.docData;
                    else if (item.serialNo == "3" || item.serialNo == "03")
                        result.F_GIUA_PHAI_MNU = item.docData;
                    else if (item.serialNo == "4" || item.serialNo == "04")
                        result.F_NHAN_PHAI_MNU = item.docData;
                    else if (item.serialNo == "5" || item.serialNo == "05")
                        result.F_UT_PHAI_MNU = item.docData;
                    else if (item.serialNo == "6" || item.serialNo == "06")
                        result.F_CAI_TRAI_MNU = item.docData;
                    else if (item.serialNo == "7" || item.serialNo == "07")
                        result.F_TRO_TRAI_MNU = item.docData;
                    else if (item.serialNo == "8" || item.serialNo == "08")
                        result.F_GIUA_TRAI_MNU = item.docData;
                    else if (item.serialNo == "9" || item.serialNo == "09")
                        result.F_NHAN_TRAI_MNU = item.docData;
                    else if (item.serialNo == "10")
                        result.F_UT_TRAI_MNU = item.docData;
                }

                if (item.docType == "FP_NOTE")
                {
                    if (item.serialNo == "1" || item.serialNo == "01")
                        result.CAI_PHAI_NOTE = item.note;
                    else if (item.serialNo == "2" || item.serialNo == "02")
                        result.TRO_PHAI_NOTE = item.note;
                    else if (item.serialNo == "3" || item.serialNo == "03")
                        result.GIUA_PHAI_NOTE = item.note;
                    else if (item.serialNo == "4" || item.serialNo == "04")
                        result.NHAN_PHAI_NOTE = item.note;
                    else if (item.serialNo == "5" || item.serialNo == "05")
                        result.UT_PHAI_NOTE = item.note;
                    else if (item.serialNo == "6" || item.serialNo == "06")
                        result.CAI_TRAI_NOTE = item.note;
                    else if (item.serialNo == "7" || item.serialNo == "07")
                        result.TRO_TRAI_NOTE = item.note;
                    else if (item.serialNo == "8" || item.serialNo == "08")
                        result.GIUA_TRAI_NOTE = item.note;
                    else if (item.serialNo == "9" || item.serialNo == "09")
                        result.NHAN_TRAI_NOTE = item.note;
                    else if (item.serialNo == "10")
                        result.UT_TRAI_NOTE = item.note;
                }

                if (item.docType == "FP_QUALITY")
                {
                    if (item.serialNo == "1" || item.serialNo == "01")
                        result.CAI_PHAI_IQL = item.quality;
                    else if (item.serialNo == "2" || item.serialNo == "02")
                        result.TRO_PHAI_IQL = item.quality;
                    else if (item.serialNo == "3" || item.serialNo == "03")
                        result.GIUA_PHAI_IQL = item.quality;
                    else if (item.serialNo == "4" || item.serialNo == "04")
                        result.NHAN_PHAI_IQL = item.quality;
                    else if (item.serialNo == "5" || item.serialNo == "05")
                        result.UT_PHAI_IQL = item.quality;
                    else if (item.serialNo == "6" || item.serialNo == "06")
                        result.CAI_TRAI_IQL = item.quality;
                    else if (item.serialNo == "7" || item.serialNo == "07")
                        result.TRO_TRAI_IQL = item.quality;
                    else if (item.serialNo == "8" || item.serialNo == "08")
                        result.GIUA_TRAI_IQL = item.quality;
                    else if (item.serialNo == "9" || item.serialNo == "09")
                        result.NHAN_TRAI_IQL = item.quality;
                    else if (item.serialNo == "10")
                        result.UT_TRAI_IQL = item.quality;
                }

                if (item.docType == "MNU_CBEFF_ROLLING")
                {
                    if (item.serialNo == "1" || item.serialNo == "01")
                        result.R_CAI_PHAI_MNU = item.docData;
                    else if (item.serialNo == "2" || item.serialNo == "02")
                        result.R_TRO_PHAI_MNU = item.docData;
                    else if (item.serialNo == "3" || item.serialNo == "03")
                        result.R_GIUA_PHAI_MNU = item.docData;
                    else if (item.serialNo == "4" || item.serialNo == "04")
                        result.R_NHAN_PHAI_MNU = item.docData;
                    else if (item.serialNo == "5" || item.serialNo == "05")
                        result.R_UT_PHAI_MNU = item.docData;
                    else if (item.serialNo == "6" || item.serialNo == "06")
                        result.R_CAI_TRAI_MNU = item.docData;
                    else if (item.serialNo == "7" || item.serialNo == "07")
                        result.R_TRO_TRAI_MNU = item.docData;
                    else if (item.serialNo == "8" || item.serialNo == "08")
                        result.R_GIUA_TRAI_MNU = item.docData;
                    else if (item.serialNo == "9" || item.serialNo == "09")
                        result.R_NHAN_TRAI_MNU = item.docData;
                    else if (item.serialNo == "10")
                        result.R_UT_TRAI_MNU = item.docData;
                }

                if (item.docType == "FULL_SLAP_LEFT")
                    result.F_SLAP_TRAI = item.docData;

                if (item.docType == "FULL_SLAP_RIGHT")
                    result.F_SLAP_PHAI = item.docData;
            }

            return result;
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
                    var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.prc_update_status_send_list_a_v5", lstParam);
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