using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Sync.Core.Helper;
using Sync.Core.Models;
using Sync.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sync.Web.Services
{
    public class PAServices : BaseServices
    {
        public static async Task<ResultQueueCNFRM> GetDataCNFRM(string procName, List<OracleParameter> lstParam)
        {

            var result = new ResultQueueCNFRM() { Data = new List<SYS_SYNC_QUEUE_CNFRM>(), ConnectDB = true };
            try
            {
                result.Data = (await databaseOracle.ExecuteProcToList<SYS_SYNC_QUEUE_CNFRM>(procName, lstParam)).ToList();
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                result.ConnectDB = databaseOracle.CheckConnect();
            }
            return result;

        }

       

        public static SyncDocumentListToA08Model GetDataXML(string procName, List<OracleParameter> lstParam, string docCode)
        {

            var result = new SyncDocumentListToA08Model() { ConnectDB = true };
            OracleConnection con = new OracleConnection(ConnectionStr);
            try
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                }
                catch (Exception)
                {
                    result.ConnectDB = false;
                    log.Error(procName + " ERROR: lỗi kết nối DB");
                    result.Success = false;
                    result.Message = "Lỗi kết nối DB";
                    return result;
                }

                using (var cmd = new OracleCommand(procName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (lstParam.Count > 0)
                        foreach (var param in lstParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    log.Error("Get xml document");
                    var resultProc = cmd.ExecuteNonQuery();
                    var p_MSG = lstParam[lstParam.Count - 1].Value.ToString();
                    if (resultProc == -1 && p_MSG == "OK")
                    {
                        result.Success = true;
                        var p_OUT_XML = (OracleClob)lstParam[lstParam.Count - 2].Value;
                        result.XML_DATA = (p_OUT_XML != null && p_OUT_XML.Value != null) ? p_OUT_XML.Value : "";
                        log.Error("Get xml document( Document code:" + docCode + " )");
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = p_MSG;
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                result = new SyncDocumentListToA08Model();
                result.Success = false;
                result.Message = ex.Message;
                if (con.State == ConnectionState.Open)
                    con.Close();
                return result;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return result;
        }

        
        public static async Task<Models.ResponseDocumentSync> SyncPersonAsync(string json, string token, string url)
        {
            var result = new Models.ResponseDocumentSync() { ConnectAPI = true };
            try
            {
                log.Info("****************************BEGIN UPDATE EPP_DOCUMENT********************************");
                log.Info("****************************JSON********************************");
                log.Info(json);
                log.Info("****************************END JSON********************************");
                //string url = "eppws/services/rest/syncTran/updateTransaction";
                HttpResponseMessage response = await ApiBase.PostJsonAsync(token, url, json);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return new Models.ResponseDocumentSync
                    {
                        success = false,
                        code = "401",
                        errorMessage = "Bạn không có quyền sử dụng API: " + url
                    };
                }
                else
                {
                    string responseString = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        var resultAPI = JsonConvert.DeserializeObject<ApiResult>(responseString);
                        if (resultAPI.code == "200" || resultAPI.code == "201")
                        {
                            result.success = true;
                        }
                        result.errorMessage = resultAPI.message;
                        result.code = resultAPI.code;
                        result.JsonAPI = responseString;
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError || response.StatusCode == System.Net.HttpStatusCode.NotImplemented
                            || response.StatusCode == System.Net.HttpStatusCode.BadGateway || response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable
                            || response.StatusCode == System.Net.HttpStatusCode.GatewayTimeout || response.StatusCode == System.Net.HttpStatusCode.HttpVersionNotSupported)
                        {
                            result.ConnectAPI = false;
                        }
                        result.success = false;
                        result.errorMessage = responseString;
                    }


                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                result.ConnectAPI = false;
                result.success = false; result.errorMessage = ex.Message;
            }
            return result;
        }

        
       
        
        
        
    }
}