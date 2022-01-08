using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Sync.Api.Models;
using Sync.Api.Models.Buf;
using Sync.Api.Utils;
using Sync.Core.Helper;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sync.Api.Services
{
    public class PAService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async Task<ApiResult> SaveDSBTuTTDH(ListBModel model)
        {
            var listBs = new ListBModels { ListB = new List<ListBModel>() };
            listBs.ListB.Add(model);
            string xmlData = "";
            var resultCreate = new Create_UpdateResponse() { ConnectDB = true };
            try
            {
                xmlData = Common.ConvertObjectToXML<ListBModels>(listBs);
                log.Info("XML data");
                log.Info(xmlData);
                List<OracleParameter> lstParam = new List<OracleParameter> { };
                lstParam.Add(new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input));
                lstParam.Add(new OracleParameter("pMsg", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output));

                var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.prc_insert_list_b_v5", lstParam);
                var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                if (resultProc == -1 && p_ERR_CODE == "OK")
                    resultCreate.message = "";
                else
                    resultCreate.message = "Lỗi thêm dữ liệu danh sách B: " + p_ERR_CODE;
            }
            catch (Exception ex)
            {
                resultCreate.message = "Lỗi thêm dữ liệu danh sách B: " + ex.Message;
                resultCreate.ConnectDB = databaseOracle.CheckConnect();
            }


            string jsonDB = JsonConvert.SerializeObject(resultCreate, Formatting.Indented);
            if (!resultCreate.ConnectDB)
            {
                throw new Exception("Không có kết nối đến Database");
            }
            await callStoreCommon.CreateLogSync(model.packageId, "B_LIST", jsonDB, resultCreate.message, (!string.IsNullOrEmpty(resultCreate.message) ? "FAIL" : "SUCCESS")).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(resultCreate.message))
            {
                await callStoreCommon.CreateLogSync(model.packageId, "B_LIST", xmlData, "XML data", (!string.IsNullOrEmpty(resultCreate.message) ? "FAIL" : "SUCCESS")).ConfigureAwait(false);
                throw new Exception(resultCreate.message);
            }
            else
            {
                await callStoreCommon.CreateQueueCNFRM("B_LIST", model.idQueue, model.packageId, "B_LIST", "DONE", "GetListB.txt").ConfigureAwait(false);
                return new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = resultCreate.message };
            }
        }
        public async Task<ApiResult> SaveDSCTuTTDH(List<HandoverC> model)
        {
            var resultCreate = new Create_UpdateResponse() { ConnectDB = true };
            if (model == null || model.Count == 0)
            {
                return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = "Thiếu dữ liệu đầu vào" };
            }
            for (int i = 0; i < model.Count; i++)
            {
                try
                {
                    var xmlData = Common.ConvertObjectToXML<HandoverC>(model[i]);
                    log.Info(DateTime.Now.ToString("yy.MM.dd HH:mm:ss") + "Get List C - " + model[i].packageId);
                    log.Info(xmlData);

                    try
                    {
                        List<OracleParameter> lstParam = new List<OracleParameter>
                            {
                                new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input),
                                new OracleParameter("pOffice", OracleDbType.Decimal, 0, ParameterDirection.Input),
                                new OracleParameter("pList", OracleDbType.Decimal, 0, ParameterDirection.Input),
                                new OracleParameter("pMsg", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output)
                            };
                        var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.prc_insert_deliver_lst_v5", lstParam);
                        var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                        if (resultProc == -1 && p_ERR_CODE == "OK")
                        {
                            resultCreate.success = true;
                            resultCreate.message = "Thêm mới ds C thành công!";
                        }
                        else
                        {
                            resultCreate.success = false;
                            resultCreate.message = "Thêm mới ds C không thành công!. Lỗi: " + p_ERR_CODE;
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                        resultCreate.ConnectDB = databaseOracle.CheckConnect();
                        resultCreate.success = false;
                        resultCreate.message = "Thêm mới ds C không thành công!. Lỗi: " + ex.Message;
                    }

                    string jsonDB = JsonConvert.SerializeObject(resultCreate, Formatting.Indented);
                    if (!resultCreate.ConnectDB)
                    {
                        resultCreate.message = "Không có kết nối database";
                        log.Error(resultCreate.message);
                        return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = resultCreate.message };
                    }
                    await callStoreCommon.CreateLogSync(model[i].packageId, "C_LIST", jsonDB, resultCreate.message, (resultCreate.success ? " SUCCESS" : " FAIL")).ConfigureAwait(false);
                    if (resultCreate.success)
                    {
                        await callStoreCommon.CreateQueueCNFRM("C_LIST", model[i].idQueue, model[i].packageId, "C_LIST", "DONE", "GetListC.txt").ConfigureAwait(false);
                    }
                    else
                    {
                        log.Error(resultCreate.message);
                        await callStoreCommon.CreateLogSync(model[i].packageId, "C_LIST", xmlData, "XML data", " FAIL").ConfigureAwait(false);
                        await callStoreCommon.CreateQueueCNFRM("C_LIST", model[i].idQueue, model[i].packageId, "C_LIST", "INIT", "GetListC.txt").ConfigureAwait(false);
                        return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = resultCreate.message };
                    }
                }
                catch (Exception ex)
                {
                    resultCreate.success = false;
                    resultCreate.message = ex.Message;
                    log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                    await callStoreCommon.CreateQueueCNFRM("C_LIST", model[i].idQueue, model[i].packageId, "C_LIST", "INIT", "GetListC.txt").ConfigureAwait(false); ;
                }
            }
            return new ApiResult() { code = resultCreate.success ? ((int)HttpStatusCode.InternalServerError).ToString() : ((int)HttpStatusCode.OK).ToString(), message = resultCreate.message };
        }
        public async Task<ApiResult> NhanThongTinHoSoDayDuTuTTDH(TransactionFull model)
        {
            // chuyen doi van tay
            if (model.transactionF != null && model.transactionF.documents.Count > 0)
            {
                model.transactionF.personAtts = new List<PersonAttachment>();
                var vanTays = model.transactionF.documents.Where(x => x.docType != "SCAN_DOCUMENT" && x.docType != "SCAN_OTHER").ToList();
                if (vanTays != null && vanTays.Count > 0)
                    model.transactionF.personAtts.Add(ChuyenVanTayHoSo(vanTays));
                model.transactionF.documents = model.transactionF.documents.Where(x => x.docType == "SCAN_DOCUMENT" || x.docType == "SCAN_OTHER").ToList();
            }

            var xmlData = Common.ConvertObjectToXML(model);

            var changeIssueOffice = string.IsNullOrEmpty(model.newPlaceOfIssue) ? 0 : 1;
            log.Info("Start insert document full");
            log.Info("XML: ");
            log.Info(xmlData);

            List<OracleParameter> lstParam = new List<OracleParameter>
                {
                    new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input),
                    new OracleParameter("pCHANGE_ISSUE_OFFICE", OracleDbType.Decimal, changeIssueOffice, ParameterDirection.Input),
                    new OracleParameter("pMsg", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output)
                };
            var resultCreate = new Create_UpdateResponse();
            try
            {
                var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.prc_insert_document_full_v5", lstParam);
                var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                if (resultProc == -1 && p_ERR_CODE == "OK")
                {
                    resultCreate.success = true;
                    resultCreate.message = "Thêm mới thông tin đầy đủ hồ sơ thành công!";
                    var resultLog = await callStoreCommon.CreateLogSync(model.transactionF.transactionId, "DOC_FULL", JsonConvert.SerializeObject(resultCreate), resultCreate.message, " SUCCESS");
                    return new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công" };
                }
                else
                {
                    resultCreate.success = false;
                    resultCreate.message = "Thêm mới thông tin đầy đủ hồ sơ không thành công!. Lỗi: " + p_ERR_CODE;
                    var resultLog = await callStoreCommon.CreateLogSync(model.transactionF.transactionId, "DOC_FULL", JsonConvert.SerializeObject(resultCreate), resultCreate.message, " FAIL");
                    return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = p_ERR_CODE };
                }
            }
            catch (Exception ex)
            {
                resultCreate.ConnectDB = databaseOracle.CheckConnect();
                resultCreate.success = false;
                resultCreate.message = "Thêm mới thông tin đầy đủ hồ sơ không thành công!. Lỗi: " + ex.Message;

                await callStoreCommon.CreateLogSync(model.transactionF.transactionId, "DOC_FULL", JsonConvert.SerializeObject(resultCreate), resultCreate.message, " FAIL");
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message };
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

        public async Task<ApiResult> SaveDetailBuf(ObjDetailBuffPerson model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.FP_01)) { var resultSync = Convert_WSQ_To_Image.Convert_WSQ(model.FP_01); if (resultSync.success) model.FP_01 = resultSync.code; }
                if (!string.IsNullOrEmpty(model.FP_02)) { var resultSync = Convert_WSQ_To_Image.Convert_WSQ(model.FP_02); if (resultSync.success) model.FP_02 = resultSync.code; }
                if (!string.IsNullOrEmpty(model.FP_03)) { var resultSync = Convert_WSQ_To_Image.Convert_WSQ(model.FP_03); if (resultSync.success) model.FP_03 = resultSync.code; }
                if (!string.IsNullOrEmpty(model.FP_04)) { var resultSync = Convert_WSQ_To_Image.Convert_WSQ(model.FP_04); if (resultSync.success) model.FP_04 = resultSync.code; }
                if (!string.IsNullOrEmpty(model.FP_05)) { var resultSync = Convert_WSQ_To_Image.Convert_WSQ(model.FP_05); if (resultSync.success) model.FP_05 = resultSync.code; }
                if (!string.IsNullOrEmpty(model.FP_06)) { var resultSync = Convert_WSQ_To_Image.Convert_WSQ(model.FP_06); if (resultSync.success) model.FP_06 = resultSync.code; }
                if (!string.IsNullOrEmpty(model.FP_07)) { var resultSync = Convert_WSQ_To_Image.Convert_WSQ(model.FP_07); if (resultSync.success) model.FP_07 = resultSync.code; }
                if (!string.IsNullOrEmpty(model.FP_08)) { var resultSync = Convert_WSQ_To_Image.Convert_WSQ(model.FP_08); if (resultSync.success) model.FP_08 = resultSync.code; }
                if (!string.IsNullOrEmpty(model.FP_09)) { var resultSync = Convert_WSQ_To_Image.Convert_WSQ(model.FP_09); if (resultSync.success) model.FP_09 = resultSync.code; }
                if (!string.IsNullOrEmpty(model.FP_10)) { var resultSync = Convert_WSQ_To_Image.Convert_WSQ(model.FP_10); if (resultSync.success) model.FP_10 = resultSync.code; }

                var buffDetails = new BuffDetails() { buffDetails = new List<ObjDetailBuffPerson>() { model } };

                var result = await SaveXmlNhanThongTinChiTietTrungAsync(buffDetails);
                if (result.code == "fail")
                {
                    await callStoreCommon.CreateQueueCNFRM("BUF_DETAIL", model.idQueue, "", "BUF_DETAIL", "INIT", "GetBufDetail.txt").ConfigureAwait(false);
                    return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = result.message };
                }
                else
                {
                    string xmlData = Common.ConvertObjectToXML<ObjDetailBuffPerson>(model);
                    await callStoreCommon.CreateLogSync(model.transactionId, "BUF_DETAIL", xmlData, "XML data", (!string.IsNullOrEmpty(result.message) ? "FAIL" : "SUCCESS")).ConfigureAwait(false); ;
                    await callStoreCommon.CreateQueueCNFRM("BUF_DETAIL", model.idQueue, "", "BUF_DETAIL", "DONE", "GetBufDetail.txt").ConfigureAwait(false);
                    return new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = result.message };
                }
            }
            catch (Exception ex)
            {
                await callStoreCommon.CreateQueueCNFRM("BUF_DETAIL", model.idQueue, "", "BUF_DETAIL", "INIT", "GetBufDetail.txt").ConfigureAwait(false);
                throw ex;
            }
        }
        private async Task<ApiResult> SaveXmlNhanThongTinChiTietTrungAsync(BuffDetails buffDetails)
        {
            var result = new ApiResult();
            try
            {
                log.Info("****************************BEGIN Insert buf detail********************************");
                string xmlData = Common.ConvertObjectToXML<BuffDetails>(buffDetails);
                log.Info("XML data");
                log.Info(xmlData);
                List<OracleParameter> lstParam = new List<OracleParameter> { };
                lstParam.Add(new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input));
                lstParam.Add(new OracleParameter("pMsg", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output));

                var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.prc_get_buffer_detail_from_ttdh_v5", lstParam);
                log.Info("****************************BEGIN Insert buf detail********************************");
                var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                if (resultProc == -1 && p_ERR_CODE == "OK")
                {
                    result.code = "success";
                    result.message = "Thêm mới dữ liệu vân tay nghi trùng thành công.";
                }
                else
                {
                    result.code = "fail";
                    result.message = "Thêm mới dữ liệu vân tay nghi trùng lỗi: " + p_ERR_CODE;
                }
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                log.Info("Insert detail buff detail lỗi: " + ex.Message);
            }

            return result;
        }
        public async Task<ApiResult> SaveXmlNhanThongTinNghiTrung(ResponseDowloadBuff model)
        {
            try
            {
                if (model != null)
                {
                    try
                    {
                        var resultCreateBuf = new ConfirmSyncDeleteModel() { ConnectDB = true };
                        try
                        {
                            log.Info("****************************BEGIN Insert buf********************************");
                            string xmlData = null;
                            if (model.listData != null && model.listData.Count > 0)
                            {
                                xmlData = Common.ConvertObjectToXML<DataPersonBuffs>(new DataPersonBuffs() { DataPersonBuff = model.listData });
                            }

                            log.Info("XML data");
                            log.Info(xmlData);
                            List<OracleParameter> lstParam = new List<OracleParameter> { };
                            lstParam.Add(new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input));
                            lstParam.Add(new OracleParameter("pNoData", OracleDbType.Decimal, string.IsNullOrEmpty(xmlData) ? 1 : 0, ParameterDirection.Input));
                            lstParam.Add(new OracleParameter("pType", OracleDbType.Varchar2, model.buffType, ParameterDirection.Input));
                            lstParam.Add(new OracleParameter("pIdQueue", OracleDbType.Decimal, model.idQueue, ParameterDirection.Input));
                            lstParam.Add(new OracleParameter("pDocumentCode", OracleDbType.Varchar2, model.transactionCode, ParameterDirection.Input));
                            lstParam.Add(new OracleParameter("pMsg", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output));

                            var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.prc_get_buffer_from_ttdh_v5", lstParam);
                            log.Info("****************************BEGIN Insert buf********************************");
                            var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                            if (resultProc == -1 && p_ERR_CODE == "OK")
                            {
                                resultCreateBuf.Success = true;
                                resultCreateBuf.Message = "Thêm mới dữ liệu nghi trùng thành công.";
                            }
                            else
                            {
                                resultCreateBuf.Success = false;
                                resultCreateBuf.Message = "Thêm mới dữ liệu nghi trùng lỗi: " + p_ERR_CODE;
                            }
                        }
                        catch (Exception ex)
                        {
                            resultCreateBuf.ConnectDB = databaseOracle.CheckConnect();
                            log.Info("Insert buf lỗi: " + ex.Message);
                            resultCreateBuf.Message = ex.Message;
                        }
                        var listCode = (model.listData != null && model.listData.Count > 0) ? string.Join(",", model.listData.Select(x => x.apxPersonCcode)) : "";
                        await callStoreCommon.CreateLogSync(model.listData != null && model.listData.Count > 0 ? model.listData.FirstOrDefault().apxPersonCcode : "", "BUF", JsonConvert.SerializeObject(model), resultCreateBuf.Message + "(" + listCode + ")", (!string.IsNullOrEmpty(resultCreateBuf.Message) ? "FAIL" : "SUCCESS")).ConfigureAwait(false);
                        if (resultCreateBuf.Success == false)
                        {
                            if (model.listData != null)
                            {
                                for (int i = 0; i < model.listData.Count; i++)
                                {
                                    var resultQueue = await callStoreCommon.CreateQueueCNFRM("BUF", model.idQueue, "", "BUF", "INIT", "GetBuf.txt").ConfigureAwait(false);
                                }
                            }

                            return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = resultCreateBuf.Message };
                        }
                        else
                        {
                            string xmlData = Common.ConvertObjectToXML<ResponseDowloadBuff>(model);
                            var resultLogXML = await callStoreCommon.CreateLogSync(model.listData != null && model.listData.Count > 0 ? model.listData.FirstOrDefault().apxPersonCcode : "", "BUF", xmlData, "XML data", (!string.IsNullOrEmpty(resultCreateBuf.Message) ? "FAIL" : "SUCCESS")).ConfigureAwait(false);
                            if (model.listData != null)
                            {
                                for (int i = 0; i < model.listData.Count; i++)
                                {
                                    var resultQueue = await callStoreCommon.CreateQueueCNFRM("BUF", model.idQueue, "", "BUF", "DONE", "GetBuf.txt").ConfigureAwait(false); ;
                                }
                            }

                            return new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = resultCreateBuf.Message };
                        }

                    }
                    catch (Exception ex)
                    {
                        if (model.listData != null)
                        {
                            for (int i = 0; i < model.listData.Count; i++)
                            {
                                var resultQueue = await callStoreCommon.CreateQueueCNFRM("BUF", model.idQueue, "", "BUF", "INIT", "GetBuf.txt").ConfigureAwait(false); ;
                            }
                        }

                        throw ex;
                    }
                }
                else
                {
                    throw new Exception("Dữ liệu đầu vào rỗng");
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                if (!databaseOracle.CheckConnect())
                    return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = "Không thể kết nối tới máy chủ." };
                else
                    return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message };
            }
        }
        public async Task<ApiResult> SaveXmlImmiHistory(List<ImmiHistory> model)
        {
            if (model != null && model.Count > 0)
            {
                try
                {
                    for (var i = 0; i < model.Count; i++)
                    {
                        model[i]._HistoryOrder = i;
                        foreach (var j in model[i].images) j._HistoryOrder = i;
                        foreach (var j in model[i].childrens) j._HistoryOrder = i;
                    }
                    var xmlData = Common.ConvertObjectToXML<List<ImmiHistory>>(model);
                    log.Info(DateTime.Now.ToString("yy.MM.dd HH:mm:ss") + "Get List ImmiHisTory - " + String.Join(",", model.Select(x => x.transactionId).ToArray()));
                    log.Info(xmlData);

                    var resultCreate = new Create_UpdateResponse() { ConnectDB = true };

                    try
                    {
                        List<OracleParameter> lstParam = new List<OracleParameter>
                            {
                                new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input),
                                new OracleParameter("pMsg", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output)
                            };
                        var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_IMMIHISTORY.prc_insert_immihistory_lst", lstParam);
                        var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                        if (resultProc == -1 && p_ERR_CODE == "OK")
                        {
                            resultCreate.success = true;
                            resultCreate.message = "Thêm mới ds thành công!";
                        }
                        else
                        {
                            resultCreate.success = false;
                            resultCreate.message = "Thêm mới ds không thành công!. Lỗi: " + p_ERR_CODE;
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                        resultCreate.ConnectDB = databaseOracle.CheckConnect();
                        resultCreate.success = false;
                        resultCreate.message = "Thêm mới ds không thành công!. Lỗi: " + ex.Message;
                    }

                    string jsonDB = JsonConvert.SerializeObject(resultCreate, Formatting.Indented);
                    if (!resultCreate.ConnectDB)
                    {
                        //await callStoreCommon.CreateLogSync(String.Join(",", model.Select(x => x.transactionId).ToArray()), "IMMIHISTORY_LIST", jsonDB, "Không có kết nối đến Database", "FAIL_DB");
                        return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = "Không có kết nối đến Database" };
                    }
                    //await callStoreCommon.CreateLogSync(String.Join(",", model.Select(x => x.transactionId).ToArray()), "IMMIHISTORY_LIST", jsonDB, resultCreate.message, (resultCreate.success ? " SUCCESS" : " FAIL")).ConfigureAwait(false);
                    //if (resultCreate.success)
                    //{
                    //    await callStoreCommon.CreateQueueCNFRM("IMMIHISTORY_LIST", null, String.Join(",", model.Select(x => x.transactionId).ToArray()), "IMMIHISTORY_LIST", "DONE", "GetListImmiHistory.txt").ConfigureAwait(false);
                    //}
                    //else
                    //{
                    //    await callStoreCommon.CreateLogSync(String.Join(",", model.Select(x => x.transactionId).ToArray()), "IMMIHISTORY_LIST", xmlData, "XML data", (resultCreate.success ? " SUCCESS" : " FAIL")).ConfigureAwait(false);
                    //    await callStoreCommon.CreateQueueCNFRM("IMMIHISTORY_LIST", null, String.Join(",", model.Select(x => x.transactionId).ToArray()), "IMMIHISTORY_LIST", "INIT", "GetListImmiHistory.txt").ConfigureAwait(false);
                    //}

                    return new ApiResult() { code = resultCreate.success ? "200" : "500", message = resultCreate.success ? "Thành công!" : "Lỗi: " + resultCreate.message };
                }
                catch (Exception ex)
                {
                    log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                    await callStoreCommon.CreateQueueCNFRM("IMMIHISTORY_LIST", null, String.Join(",", model.Select(x => x.transactionId).ToArray()), "IMMIHISTORY_LIST", "INIT", "GetListImmiHistory.txt").ConfigureAwait(false);
                    return new ApiResult() { code = "500", message = "Lỗii: " + ex.Message };
                }

            }
            else
            {
                return new ApiResult() { code = "200", message = "Dữ liệu truyền vào rỗng! Không hành động nào được thực thi." };
            }
        }
    }
}