using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Sync.Api.Models;
using Sync.Api.Models.Buf;
using Sync.Core.Helper;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sync.Api.Services
{
    public class AService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<ApiResult> SaveDSATuTTDH(List<HandoverA> model)
        {
            var resultCreate = new Create_UpdateResponse();
            try
            {
                if (model.FirstOrDefault().receipts.Count > 0)
                {
                    if (model.Any(x => x.receipts.Any(y => y.handovers == null || y.handovers.Count() == 0)))
                    {
                        log.Info("================= Lấy danh sách A =================");
                        log.Info(JsonConvert.SerializeObject(model));
                        log.Info("================= Kết thúc lấy danh sách A =================");
                        return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = "Thiếu thông tin hồ sơ" };
                    }
                    string xmlData = Common.ConvertObjectToXML<List<HandoverA>>(model);
                    log.Info("================= Lấy danh sách A =================");
                    log.Info(xmlData);
                    log.Info(JsonConvert.SerializeObject(model));
                    log.Info("================= Kết thúc lấy danh sách A =================");
                    List<OracleParameter> lstParam = new List<OracleParameter>
                {
                    new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input),
                    new OracleParameter("pMsg", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output)
                };
                    var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.INSERT_ALIST_FRM_NPCC_V5", lstParam);
                    var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                    if (resultProc == -1 && p_ERR_CODE == "OK")
                    {
                        resultCreate.success = true;
                        resultCreate.message = "SUCC - " + model.FirstOrDefault().packageId;
                    }
                    else
                    {
                        resultCreate.success = false;
                        if (p_ERR_CODE == "NOK")
                        {
                            resultCreate.message = "RECALL - " + model.FirstOrDefault().packageId + " - Thiếu thông tin hồ sơ!";
                            resultCreate.code = "NOK";
                        }
                        else if (p_ERR_CODE == "ROK")
                        {
                            resultCreate.message = "FAIL - " + model.FirstOrDefault().packageId + " - Thiếu thông tin biên nhận!";
                            resultCreate.code = "ROK";
                        }
                        else if (p_ERR_CODE == "LOK")
                        {
                            resultCreate.message = "FAIL - " + model.FirstOrDefault().packageId + " - Danh sách đã tồn tại!";
                            resultCreate.code = "LOK";
                        }
                        else
                        {
                            resultCreate.message = "FAIL - " + model.FirstOrDefault().packageId + " - " + p_ERR_CODE;
                        }
                    }
                }
                else
                {
                    resultCreate.success = false;
                    resultCreate.message = "FAIL - " + model.FirstOrDefault().packageId + " - Thiếu thông tin biên nhận !";
                }

            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");

                resultCreate.success = false;
                resultCreate.message = "Thêm mới không thành công!. Lỗi: " + ex.Message + "-" + model.FirstOrDefault().packageId;
            }

            string status = resultCreate.code == "NOK" ? "RECALL" : "INIT";
            if (resultCreate.success)
            {
                status = "DONE";
            }
            else
            {
                string xmlData = Common.ConvertObjectToXML<List<HandoverA>>(model);
                _ = callStoreCommon.CreateLogSync("", "A_LIST", xmlData, "XML data", (status != "DONE" ? "FAIL" : "SUCCESS"));
            }

            string jsonDB = JsonConvert.SerializeObject(resultCreate, Newtonsoft.Json.Formatting.Indented);
            _ = callStoreCommon.CreateLogSync("", "A_LIST", jsonDB, resultCreate.message, (status != "DONE" ? "FAIL" : "SUCCESS"));

            for (int i = 0; i < model.Count; i++)
            {
                _ = callStoreCommon.CreateQueueCNFRM("A_LIST", model[i].idQueue, model[i].packageId, "A_LIST", status, "GetListA.txt");
            }

            return new ApiResult() { code = resultCreate.success ? ((int)HttpStatusCode.OK).ToString() : ((int)HttpStatusCode.InternalServerError).ToString(), message = resultCreate.message };
        }

        public async Task<ApiResult> SaveDSCTuTTDH(List<HandoverC> model)
        {
            var listRs = new List<HandoverResultModel>();
            if (model != null && model.Count > 0)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    try
                    {
                        var xmlData = Common.ConvertObjectToXML<HandoverC>(model[i]);
                        log.Info(DateTime.Now.ToString("yy.MM.dd HH:mm:ss") + "Get List C - " + model[i].packageId);
                        log.Info(xmlData);

                        var resultCreate = new Create_UpdateResponse() { ConnectDB = true };
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
                            await callStoreCommon.CreateLogSync(model[i].packageId, "C_LIST", jsonDB, "Không có kết nối đến Database", "FAIL_DB");
                            return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = "Không có kết nối đến Database" };
                        }

                        await callStoreCommon.CreateLogSync(model[i].packageId, "C_LIST", jsonDB, resultCreate.message, (resultCreate.success ? " SUCCESS" : " FAIL"));

                        if (resultCreate.success)
                        {
                            await callStoreCommon.CreateQueueCNFRM("C_LIST", model[i].idQueue, model[i].packageId, "C_LIST", "DONE", "GetListC.txt");
                        }
                        else
                        {
                            await callStoreCommon.CreateLogSync(model[i].packageId, "C_LIST", xmlData, "XML data", (resultCreate.success ? " SUCCESS" : " FAIL"));
                            await callStoreCommon.CreateQueueCNFRM("C_LIST", model[i].idQueue, model[i].packageId, "C_LIST", "INIT", "GetListC.txt");
                        }
                        var tempRs = new HandoverResultModel() { packageId = model[i].packageId, idQueue = model[i].idQueue, result = resultCreate };
                        listRs.Add(tempRs);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                        await callStoreCommon.CreateQueueCNFRM("C_LIST", model[i].idQueue, model[i].packageId, "C_LIST", "INIT", "GetListC.txt");
                    }
                }
            }
            var errorCode = String.Join(",", listRs.Where(x => x.result.success == false && x.idQueue != 0).Select(x => x.idQueue.ToString()).ToArray());
            return new ApiResult() { code = (model == null || model.Count == 0 || listRs.Any(x => x.result.success == false)) ? ((int)HttpStatusCode.InternalServerError).ToString() : ((int)HttpStatusCode.OK).ToString(), message = errorCode };
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
                                var checkData = model.listData.Where(x => !string.IsNullOrEmpty(x.apxPersonCcode) || !string.IsNullOrEmpty(x.maCaNhan)).ToList();
                                if (checkData != null && checkData.Count > 0)
                                {
                                    xmlData = Common.ConvertObjectToXML<DataPersonBuffs>(new DataPersonBuffs() { DataPersonBuff = checkData });
                                }
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
                                if (p_ERR_CODE == "NOT_HS")
                                    resultCreateBuf.Message = "Không có chủ hồ sơ.";
                                else
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
    }
}