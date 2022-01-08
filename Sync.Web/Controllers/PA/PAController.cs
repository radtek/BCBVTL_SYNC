using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Sync.Core.Helper;
using Sync.Core.Models;
using Sync.Web.Models;
using Sync.Web.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sync.Web.Controllers.PA
{
    public class PAController : BaseController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string accessToken = "";

        // GET: PA
        public ActionResult Index()
        {
            return View();
        }

       
        public async Task<ApiResult> GuiThongTinHoSoLenTTDH()
        {
            ApiResult apiResult = new ApiResult();
            var listData = new List<SYS_SYNC_QUEUE_CNFRM>();

            try
            {
                List<OracleParameter> lstParam = new List<OracleParameter>
                {
                    new OracleParameter("p_OUT_LIST", OracleDbType.RefCursor, ParameterDirection.Output)
                };
                var datas = await PAServices.GetDataCNFRM("EPA_UPLOAD_SYNC_TO_TTDH.PRC_SYNC_GET_DOCUMENT_TO_TTDH", lstParam);

                if (datas != null && datas.Data != null && datas.Data.Count > 0)
                    listData = datas.Data;

                var listCode = (datas.Data != null && datas.Data.Count > 0) ? string.Join(",", datas.Data.Select(x => x.Object_Code)) : "";
                string jsonDB = JsonConvert.SerializeObject(datas, Formatting.Indented);

                var resultLogGetData = await callStoreCommon.CreateLogSync("", "DOC", jsonDB, (listData != null && listData.Count > 0) ? "Lấy danh sách hồ sơ cần đẩy lên TTĐH thành công." : "Không có hồ sơ cần đẩy lên TTĐH.", ((listData != null && listData.Count > 0) ? " SUCCESS" : " FAIL"));

                if (listData != null && listData.Count > 0)
                {
                    for (int i = 0; i < listData.Count; i++)
                    {
                        List<OracleParameter> lstParam1 = new List<OracleParameter>
                        {
                            new OracleParameter("queueId", OracleDbType.Decimal, (long)listData[i].Id, ParameterDirection.Input),
                            new OracleParameter("pXml", OracleDbType.Clob,100000, ParameterDirection.Output),
                            new OracleParameter("pMsg", OracleDbType.Varchar2,2000,"", ParameterDirection.Output)
                        };

                        var uploadInfo = PAServices.GetDataXML("EPA_UPLOAD_SYNC_TO_TTDH.Get_doc_xml_data", lstParam1, listData[i].Object_Code);


                        string jsonDBXML = JsonConvert.SerializeObject(datas, Formatting.Indented);

                        if (uploadInfo.ConnectDB == false)
                        {
                            apiResult.message = $"{DateTime.Now:yy.MM.dd HH:mm:ss} - Không có kết nối đến Database";
                            var resultLog1 = await callStoreCommon.CreateLogSync(listData[i].Object_Code, "DOC", jsonDB, "Không có kết nối đến Database", "FAIL_DB").ConfigureAwait(false); ;
                        }

                        // Ghi log thông tin thêm dữ liệu vào DB
                        var resultLogGetXML = await callStoreCommon.CreateLogSync(listData[i].Object_Code, "DOC", jsonDBXML, !string.IsNullOrEmpty(uploadInfo.XML_DATA) ? "Lấy xml thành công." : "Lấy xml không thành công.", (!string.IsNullOrEmpty(uploadInfo.XML_DATA) ? " SUCCESS" : " FAIL")).ConfigureAwait(false);

                        if (!string.IsNullOrEmpty(uploadInfo.XML_DATA))
                        {
                            try
                            {
                                List<string> nodeList = new List<string>() { "documents", "families" };
                                // Convert xml to model
                                var strJson = Common.XmlToJSON(uploadInfo.XML_DATA, nodeList);
                                var length = strJson.IndexOf(":");
                                string strJson1 = strJson.Substring(0, length + 1);
                                string strJson2 = strJson.Replace(strJson1, "");
                                string strJson3 = strJson2.Substring(0, strJson2.Length - 1);
                                if (!string.IsNullOrEmpty(strJson3))
                                {
                                    try
                                    {
                                        var isConnectApi = await BaseServices.CheckConnectAPIAsync(maTrungTam);
                                        if (isConnectApi.code == "200")
                                        {
                                            var resultDL = await PAServices.SyncPersonAsync(strJson3, accessToken, "eppws/services/rest/syncTran/uploadTransaction");

                                            string jsonAPI = "";
                                            if (!string.IsNullOrEmpty(resultDL.JsonAPI))
                                                jsonAPI = resultDL.JsonAPI;
                                            else
                                                jsonAPI = JsonConvert.SerializeObject(resultDL, Formatting.Indented);

                                            if (resultDL.ConnectAPI == false)
                                            {
                                                var resultLogApiFalse = await callStoreCommon.CreateLogSync(listData[i].Object_Code, "DOC", jsonAPI, "Không có kết nối đến API", "FAIL_API");
                                            }

                                            // Ghi log thông tin API trả lại
                                            var resultLog = await callStoreCommon.CreateLogSync(listData[i].Object_Code, "DOC", jsonAPI, resultDL.errorMessage, resultDL.code).ConfigureAwait(false);

                                            if (!resultDL.success)
                                            {
                                                var resultLogJson = await callStoreCommon.CreateLogSync(listData[i].Object_Code, "DOC", strJson3, "Json call API", resultDL.code).ConfigureAwait(false); ;
                                                var update = await callStoreCommon.UpdateStatusQueuePro(listData[i].Id, "INIT");
                                                apiResult.code = listData[i].Object_Code;
                                                apiResult.message = $"{DateTime.Now:yy.MM.dd HH:mm:ss} - FAIL - {listData[i].Object_Code} - {resultDL.errorMessage}";
                                            }
                                            else
                                            {
                                                var update = await callStoreCommon.UpdateStatusQueuePro(listData[i].Id, "DONE").ConfigureAwait(false);
                                                if (update.success == false)
                                                {
                                                    apiResult.message = $"{DateTime.Now:yy.MM.dd HH:mm:ss} {update}";
                                                }
                                                else
                                                {
                                                    apiResult.message = $"{DateTime.Now:yy.MM.dd HH:mm:ss} Đồng bộ thành công { listData[i].Object_Code}";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            apiResult.message = $"{DateTime.Now:yy.MM.dd HH:mm:ss} - Không có kết nối với TTĐH";
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                                        apiResult.message = $"{DateTime.Now:yy.MM.dd HH:mm:ss} - Không kết nối được API của TTĐH";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                                var update = await callStoreCommon.UpdateStatusQueuePro(listData[i].Id, "INIT").ConfigureAwait(false);
                            }
                        }
                        else
                        {
                            var update = await callStoreCommon.UpdateStatusQueuePro(listData[i].Id, "INIT").ConfigureAwait(false); ;
                            apiResult.message = $"{DateTime.Now:yy.MM.dd HH:mm: ss} - Không lấy được dữ liệu XML";
                        }
                    }
                }
                else
                {
                    apiResult.message = $"{DateTime.Now:yy.MM.dd HH:mm:ss} - Không có hồ sơ gửi lên TTDH";
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                apiResult.message = $"{DateTime.Now:yy.MM.dd HH:mm:ss} {ex.Message}";
            }

            return apiResult;
        }

    }
}