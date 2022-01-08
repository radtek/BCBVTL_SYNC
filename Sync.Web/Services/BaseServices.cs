using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Sync.Core.Helper;
using Sync.Core.Models;
using Sync.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sync.Web.Services
{
    public class BaseServices
    {
        protected static string ConnectionStr = EppMaHoa.CommonManager.DecryptSHA256(ConfigurationManager.AppSettings.Get("ConnectionString"));//MaHoa_GiaiMa.Decrypt(ConfigurationManager.AppSettings.Get("ConnectionString"));
        protected static DatabaseOracle databaseOracle = new DatabaseOracle(ConnectionStr);
        protected CallStoreCommon callStoreCommon = new CallStoreCommon(ConnectionStr);
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static async Task<ApiResult> SyncInfoAsync(string json, string token, string url)
        {
            try
            {
                var resultSync = new ApiResult();

                log.Info("JSON: " + json);
                HttpResponseMessage response = await ApiBase.PostJsonAsync(token, url, json);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new ApiResult
                    {
                        code = "401",
                        message = $"Bạn không có quyền sử dụng API: {url}"
                    };
                }
                else
                {
                    string responseString = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        resultSync = JsonConvert.DeserializeObject<ApiResult>(responseString);
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.InternalServerError || response.StatusCode == HttpStatusCode.NotImplemented
                            || response.StatusCode == HttpStatusCode.BadGateway || response.StatusCode == HttpStatusCode.ServiceUnavailable
                            || response.StatusCode == HttpStatusCode.GatewayTimeout || response.StatusCode == HttpStatusCode.HttpVersionNotSupported)
                        {
                        }
                        return new ApiResult() { code = response.StatusCode.ToString(), message = responseString };
                    }

                    log.Info("****************************END UPLOAD DOCUMENT LIST TO TTĐH********************************");
                    return resultSync;
                }

            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return new ApiResult { code = "501", message = ex.Message };
            }
        }

        // <summary>
        /// Kiểm tra kết nối API
        /// </summary>
        /// <returns>
        /// code: 200 (có kết nối)
        /// code: 408: không kết nối
        /// </returns>
        public static async Task<ApiResult> CheckConnectAPIAsync(string maTrungTam)
        {
            var resultApi = new ApiResult();
            try
            {
                log.Info("****************************Check connect API********************************");
                HttpResponseMessage response = await ApiBase.GetCheckConnectAPI(maTrungTam);

                string responseString = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
                log.Info(responseString);
                resultApi = JsonConvert.DeserializeObject<ApiResult>(responseString);
                log.Info("****************************End Check connect API********************************");
                return resultApi;

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return new ApiResult { code = "408", message = ex.Message };
            }
        }

        public static async Task<List<ReSendDataModel>> GetDataToResend(string typeData)
        {
            var result = new List<ReSendDataModel>();
            try
            {
                List<OracleParameter> lstParam = new List<OracleParameter>
                {
                     new OracleParameter("p_TYPE_DATA", OracleDbType.Varchar2, typeData, ParameterDirection.Input),
                     new OracleParameter("p_OUT_DATA", OracleDbType.RefCursor, ParameterDirection.Output)
                };

                result = (await databaseOracle.ExecuteProcToList<ReSendDataModel>("EPA_UPLOAD_SYNC_TO_TTDH.PRC_GET_Queue_ReSend", lstParam)).ToList();
            }
            catch (Exception ex)
            {
                log.Error("ReSendData -> GetData: " + ex.Message);
            }

            return result;

        }

        public static async Task<string> UpdateQueueToResendData(string listQueueId)
        {
            try
            {
                List<OracleParameter> lstParam = new List<OracleParameter>
                {
                     new OracleParameter("p_LIST_QUEUE_ID", OracleDbType.Varchar2, listQueueId, ParameterDirection.Input),
                     new OracleParameter("p_ERR_CODE", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output),
                     new OracleParameter("p_ERR_MESSAGE", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output)
                };

                int resultProc = 0;

                resultProc = await databaseOracle.ExecuteProcNonQuery("EPA_UPLOAD_SYNC_TO_TTDH.PRC_Update_Queue_ReSend", lstParam);

                var p_ERR_CODE = lstParam[lstParam.Count - 2].Value.ToString();
                var p_ERR_MESSAGE = lstParam[lstParam.Count - 1].Value.ToString();
                if (resultProc == -1 && p_ERR_CODE == "OK")
                    return "";
                else
                    return "Cập nhật hàng đợi lỗi: " + p_ERR_MESSAGE;
            }
            catch (Exception ex)
            {
                log.Error("ReSendData -> GetData: " + ex.Message);
                return "Cập nhật hàng đợi lỗi: " + ex.Message;
            }
        }

        public static async Task<ResultRecallInfoModel> GetDataApiToRecall()
        {
            var result = new ResultRecallInfoModel() { Data = new List<RecallInfoModel>(), ConnectDB = true };

            try
            {
                List<OracleParameter> lstParam = new List<OracleParameter>
                {
                    new OracleParameter("p_OUT_LIST", OracleDbType.RefCursor, ParameterDirection.Output)
                };
                result.Data = (await databaseOracle.ExecuteProcToList<RecallInfoModel>("EPA_UPLOAD_SYNC_TO_TTDH.PRC_GET_RECALL_API_DATA", lstParam)).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                result.ConnectDB = databaseOracle.CheckConnect();
            }
            return result;
        }

        public static async Task<string> DeleteExcecutedItem(decimal id, string reftable)
        {
            try
            {
                List<OracleParameter> lstParam = new List<OracleParameter> { };
                lstParam.Add(new OracleParameter("p_ID", OracleDbType.Decimal, id, ParameterDirection.Input));
                lstParam.Add(new OracleParameter("p_REFTABLE", OracleDbType.Varchar2, reftable, ParameterDirection.Input));
                lstParam.Add(new OracleParameter("p_ERR_CODE", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output));
                lstParam.Add(new OracleParameter("p_ERR_MESSAGE", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output));

                var resultProc = await databaseOracle.ExecuteProcNonQuery("EPA_UPLOAD_SYNC_TO_TTDH.PRC_DELETE_EXECUTED_API", lstParam);
                var p_ERR_CODE = lstParam[lstParam.Count - 2].Value.ToString();
                if (resultProc == -1 && p_ERR_CODE == "OK")
                    return "Xóa hàng đợi thành công";
                else
                {
                    var p_ERR_MESSAGE = lstParam[lstParam.Count - 1].Value.ToString();
                    return "Xóa hàng đợi lỗi, chi tiết:" + p_ERR_MESSAGE;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public static async Task<ApiResult> RecallApi()
        {
            var resultApi = new ApiResult();
            var result = await GetDataApiToRecall();

            try
            {
                if (result == null)
                {
                    log.Info("Không có Api nào cần gọi lại...");
                    resultApi.message = "Không có Api nào cần gọi lại...";
                }
                else
                {
                    if (result.Data != null && result.Data.Count > 0)
                    {
                        foreach (var item in result.Data)
                        {
                            if (item.Result.Contains("414") == false)
                            {
                                var connectAPI = true;
                                var json = item.Body;
                                var url = item.Url;
                                var resultSync = SyncInfo(json, "", url, item.Apitype, ref connectAPI);
                                if (resultSync.code == "200" || resultSync.code == "201")
                                {
                                    await DeleteExcecutedItem(item.Id, item.Reftable);
                                    resultApi.message = "Gọi lại API thành công";
                                }
                            }
                        }
                    }
                    else
                    {
                        resultApi.message = "Gọi lại API không thành công";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                resultApi.message = ex.Message;

            }

            return resultApi;
        }

        public static ApiResult SyncInfo(string json, string token, string url, string apitype, ref bool connectAPI)
        {
            connectAPI = true;
            try
            {
                var resultSync = new ApiResult();
                //string json = JsonConvert.SerializeObject(infoSync, Formatting.Indented);
                log.Info("****************************BEGIN RECALL API********************************");
                log.Info("JSON: " + json);
                if (apitype == "POST")
                {
                    HttpResponseMessage response = (ApiBase.PostJsonAsync(token, url, json)).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        log.Info("****************************END RECALL API********************************");
                        return new ApiResult
                        {
                            code = "401",
                            message = "Bạn không có quyền sử dụng API này"
                        };
                    }
                    else
                    {
                        string responseString = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
                        log.Info(responseString);
                        if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                        {
                            resultSync = JsonConvert.DeserializeObject<ApiResult>(responseString);

                        }
                        else
                        {
                            if (response.StatusCode == HttpStatusCode.InternalServerError || response.StatusCode == HttpStatusCode.NotImplemented
                                || response.StatusCode == HttpStatusCode.BadGateway || response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable
                                || response.StatusCode == HttpStatusCode.GatewayTimeout || response.StatusCode == System.Net.HttpStatusCode.HttpVersionNotSupported)
                            {
                                connectAPI = false;
                            }
                            resultSync.code = response.StatusCode.ToString();
                            resultSync.message = responseString;
                        }
                        log.Info("****************************END RECALL API*******************************");
                        return resultSync;
                    }
                }
                else if (apitype == "PUSH")
                {
                    HttpResponseMessage response = (ApiBase.PutJsonAsyncResponse(token, url, json)).Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.Info("****************************END RECALL API********************************");
                        return new ApiResult
                        {
                            code = "401",
                            message = "Bạn không có quyền sử dụng API này"
                        };
                    }
                    else
                    {
                        string responseString = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
                        log.Info(responseString);
                        if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            resultSync = JsonConvert.DeserializeObject<ApiResult>(responseString);

                        }
                        else
                        {
                            if (response.StatusCode == HttpStatusCode.InternalServerError || response.StatusCode == HttpStatusCode.NotImplemented
                                || response.StatusCode == HttpStatusCode.BadGateway || response.StatusCode == HttpStatusCode.ServiceUnavailable
                                || response.StatusCode == HttpStatusCode.GatewayTimeout || response.StatusCode == HttpStatusCode.HttpVersionNotSupported)
                            {
                                connectAPI = false;
                            }
                            resultSync.code = response.StatusCode.ToString();
                            resultSync.message = responseString;
                        }
                        log.Info("****************************END RECALL API*******************************");
                        return resultSync;
                    }
                }
                return resultSync;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return new ApiResult { code = "404", message = "Lỗi xử lý dữ liệu." };
            }
        }
    }
}