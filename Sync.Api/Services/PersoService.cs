using log4net;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Sync.Api.Models;
using Sync.Api.Models.SyncCategory;
using Sync.Core.Helper;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Sync.Api.Services
{
    public class PersoService : BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async Task<ApiResult> SaveDSBTuTTDH(documentList model)
        {
            if (model != null && model.listPassportInfo.Count > 0)
            {
                try
                {
                    var resultCreate = new Create_UpdateResponse() { ConnectDB = true };
                    try
                    {
                        var xml = Common.ConvertObjectToXML<documentList>(model);
                        log.Info(DateTime.Now.ToString("yy.MM.dd HH:mm:ss") + "Get List B - " + model.handoverId);
                        log.Info(xml);
                        var stringtexxt = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
                        xml = xml.Replace(stringtexxt, "");

                        List<OracleParameter> lstParam = new List<OracleParameter>
                        {
                            new OracleParameter("pXmlData", OracleDbType.Clob, xml, ParameterDirection.Input),
                            new OracleParameter("pMsg", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output)
                        };
                        log.Info("****************************Bắt đầu Insert dữ liệu danh sách B từ Trung tâm xử lý(PKG_SYNC_DATA_TTXL.prc_insert_prp_perso_lst)********************************");
                        var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_DATA_TTXL.prc_insert_prp_perso_lst", lstParam);
                        var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                        if (resultProc == -1 && p_ERR_CODE == "OK")
                        {
                            resultCreate.success = true;
                            resultCreate.message = "Thêm mới thành công!";
                        }
                        else
                        {
                            resultCreate.success = false;
                            resultCreate.message = "Thêm mới không thành công!. Lỗi: " + p_ERR_CODE;
                            log.Error("Lỗi: " + p_ERR_CODE);
                        }
                        log.Info("****************************Kết thúc Insert dữ liệu danh sách B từ Trung tâm xử lý(PKG_SYNC_DATA_TTXL.prc_insert_prp_perso_lst)********************************");

                    }
                    catch (Exception ex)
                    {
                        resultCreate.ConnectDB = databaseOracle.CheckConnect();
                        log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                        resultCreate.success = false;
                        resultCreate.message = "Thêm mới ds C không thành công!. Lỗi: " + ex.Message;
                    }

                    string jsonDB = JsonConvert.SerializeObject(resultCreate, Formatting.Indented);
                    await callStoreCommon.CreateLogSync(model.handoverId, "B_LIST", JsonConvert.SerializeObject(model), resultCreate.message, resultCreate.success.ToString());
                    if (resultCreate.success)
                    {
                        await callStoreCommon.CreateQueueCNFRM("B_LIST", model.idQueue, model.handoverId, "B_LIST", "DONE", "GetListB.txt");
                        return new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công" };
                    }
                    else
                    {
                        await callStoreCommon.CreateQueueCNFRM("B_LIST", model.idQueue, model.handoverId, "B_LIST", "INIT", "GetListB.txt");
                        return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = resultCreate.message };
                    }
                }
                catch (Exception ex)
                {
                    await callStoreCommon.CreateQueueCNFRM("B_LIST", model.idQueue, model.handoverId, "B_LIST", "INIT", "GetListB.txt");
                    log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                    return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message };
                }
            }
            else
            {
                return new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = "Danh sách truyền vào rỗng." };
            }

        }

        internal async Task<ApiResult> SaveTTDanhMucTuTTDHAsync(ListDatas model)
        {
            if (model != null)
            {
                var listDatas = new ListDatas
                {
                    listCountry = new List<CategoryData>(),
                    listDistrict = new List<DistrictData>(),
                    listPurpose = new List<PurposeData>(),
                    listBordergate = new List<BordergatData>(),
                    listOffice = new List<OfficeData>(),
                    listOrganization = new List<OrganizationData>(),
                    listFeeInfo = new List<FeeInfoData>(),
                    listEthnic = new List<ReligionData>(),
                    listReligion = new List<ReligionData>(),
                    listArea = new List<DistrictData>()
                };

                if (model.listCountry != null && model.listCountry.Count > 0)
                    listDatas.listCountry.AddRange(model.listCountry);

                if (model.listPurpose != null && model.listPurpose.Count > 0)
                    listDatas.listPurpose.AddRange(model.listPurpose);

                if (model.listBordergate != null && model.listBordergate.Count > 0)
                    listDatas.listBordergate.AddRange(model.listBordergate);

                if (model.listOffice != null && model.listOffice.Count > 0)
                    listDatas.listOffice.AddRange(model.listOffice);

                if (model.listOrganization != null && model.listOrganization.Count > 0)
                    listDatas.listOrganization.AddRange(model.listOrganization);

                if (model.listFeeInfo != null && model.listFeeInfo.Count > 0)
                    listDatas.listFeeInfo.AddRange(model.listFeeInfo);

                if (model.listEthnic != null && model.listEthnic.Count > 0)
                    listDatas.listEthnic.AddRange(model.listEthnic);

                if (model.listReligion != null && model.listReligion.Count > 0)
                    listDatas.listReligion.AddRange(model.listReligion);

                if (model.listArea != null && model.listArea.Count > 0)
                    listDatas.listArea.AddRange(model.listArea);

                if (model.listDistrict != null && model.listDistrict.Count > 0)
                    listDatas.listDistrict.AddRange(model.listDistrict);

                var resultCreateBuf = new ConfirmSyncDeleteModel() { ConnectDB = true };
                try
                {
                    log.Info("****************************BEGIN thêm dữ liệu danh mục********************************");
                    string xmlData = Common.ConvertObjectToXML<ListDatas>(model);
                    log.Info("XML data");
                    log.Info(xmlData);
                    List<OracleParameter> lstParam = new List<OracleParameter> { };
                    lstParam.Add(new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input));
                    lstParam.Add(new OracleParameter("pMsg", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output));

                    var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_DATA_TTXL.prc_cap_nhat_danh_muc", lstParam);
                    log.Info("****************************BEGIN thêm dữ liệu danh mục********************************");
                    var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                    if (resultProc == -1 && p_ERR_CODE == "OK")
                    {
                        resultCreateBuf.Message = "Thành công";
                        resultCreateBuf.Success = true;
                    }
                    else
                    {
                        resultCreateBuf.Success = false;
                        resultCreateBuf.Message = "Lỗi cập nhật dữ liệu danh mục: " + p_ERR_CODE;
                    }

                }
                catch (Exception ex)
                {
                    resultCreateBuf.Success = false;
                    resultCreateBuf.Message = "Lỗi cập nhật dữ liệu danh mục: " + ex.Message;
                    resultCreateBuf.ConnectDB = databaseOracle.CheckConnect();
                }

                var resultLog = await callStoreCommon.CreateLogSync("", "STATUS_A_LIST", JsonConvert.SerializeObject(model), resultCreateBuf.Message, (!string.IsNullOrEmpty(resultCreateBuf.Message) ? "FAIL" : "SUCCESS"));
                if (resultCreateBuf.Success)
                {
                    return new ApiResult() { code = "200", message = resultCreateBuf.Message };
                }
                else
                {
                    return new ApiResult() { code = "500", message = resultCreateBuf.Message };
                }
            }
            else
            {
                return new ApiResult() { code = "500", message = "Không có dữ liệu đầu vào!" };
            }
        }

        internal async Task<ResponseList<ttImageListResultModel>> SaveAnhSuaTuTTDHAsync(List<ttImages> model)
        {
            List<ttImageListResultModel> listRs = new List<ttImageListResultModel>();
            if (model != null && model.Count > 0)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    try
                    {
                        var resultCreate = new Create_UpdateResponse() { ConnectDB = true };
                        try
                        {
                            List<OracleParameter> lstParam = new List<OracleParameter>();
                            lstParam.Add(new OracleParameter("p_DOC_CODE", OracleDbType.Varchar2, model[i].transactionId, ParameterDirection.Input));
                            lstParam.Add(new OracleParameter("p_BASE64", OracleDbType.Clob, model[i].photo, ParameterDirection.Input));
                            lstParam.Add(new OracleParameter("p_OUT", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output));
                            decimal response = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_DATA_TTXL.PRC_UPDATE_IMG_TTDH", lstParam);
                            string p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                            if (response == -1 && p_ERR_CODE == "Ok")
                            {
                                resultCreate.success = true;
                                resultCreate.message = "Thêm mới thành công!";
                            }
                            else
                            {
                                resultCreate.success = false;
                                resultCreate.message = "Thêm mới không thành công!. Lỗi: " + p_ERR_CODE;
                                log.Info("Lỗi: " + p_ERR_CODE);
                            }
                            log.Info("****************************Kết thúc Insert dữ liệu ảnh từ ttdh********************************");

                        }
                        catch (Exception ex)
                        {
                            resultCreate.ConnectDB = databaseOracle.CheckConnect();
                            log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                            resultCreate.success = false;
                            resultCreate.message = "Thêm mới ds C không thành công!. Lỗi: " + ex.Message;
                        }

                        string jsonDB = JsonConvert.SerializeObject(resultCreate, Formatting.Indented);
                        await callStoreCommon.CreateLogSync(model[i].transactionId, "IMAGES", JsonConvert.SerializeObject(model), resultCreate.message, resultCreate.success.ToString());

                        if (resultCreate.success)
                        {
                            await callStoreCommon.CreateQueueCNFRM("IMAGES", model[i].idQueue, model[i].transactionId, "IMAGES", "DONE", "GeImages.txt");
                        }
                        else
                        {
                            log.Error("Fail: " + resultCreate.message);
                            await callStoreCommon.CreateQueueCNFRM("IMAGES", model[i].idQueue, model[i].transactionId, "IMAGES", "INIT", "GeImages.txt");
                        }
                        var tempRs = new ttImageListResultModel() { transactionId = model[i].transactionId, idQueue = model[i].idQueue, result = resultCreate };
                        listRs.Add(tempRs);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                        await callStoreCommon.CreateQueueCNFRM("IMAGES", model[i].idQueue, model[i].transactionId, "IMAGES", "INIT", "GeImages.txt");
                    }
                }
            }
            else
            {
                return new ResponseList<ttImageListResultModel>() { code = ((int)HttpStatusCode.InternalServerError).ToString(), data = listRs };
            }

            return new ResponseList<ttImageListResultModel>() { code = (model == null || model.Count == 0 || listRs.Any(x => x.result.success == false)) ? ((int)HttpStatusCode.InternalServerError).ToString() : ((int)HttpStatusCode.OK).ToString(), data = listRs };
        }

        public async Task<ApiResult> GetPassPortReturn(List<PPReturnData> model)
        {
            try
            {
                var resultCreateBuf = new ConfirmSyncDeleteModel() { ConnectDB = true };
                if (model != null && model.Count > 0)
                {
                    var data = new PPReturnDatas() { PPReturnData = model };
                    var xml = Common.ConvertObjectToXML<PPReturnDatas>(data);

                    List<OracleParameter> lstParam = new List<OracleParameter>
                    {
                        new OracleParameter("pXmlData", OracleDbType.Clob, xml, ParameterDirection.Input),
                        new OracleParameter("pMsg", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output)
                    };
                    log.Info("****************************Bắt đầu Insert data list passport return********************************");
                    var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_DATA_TTXL.prc_insert_pp_return", lstParam);
                    var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                    if (resultProc == -1 && p_ERR_CODE == "OK")
                    {
                        resultCreateBuf.Success = true;
                        resultCreateBuf.Message = "Thêm mới thành công!";
                    }
                    else
                    {
                        resultCreateBuf.Success = false;
                        resultCreateBuf.Message = "Thêm mới không thành công!. Lỗi: " + p_ERR_CODE;
                        log.Info("Lỗi: " + p_ERR_CODE);
                    }
                    log.Info("****************************Kết thúc Insert data list passport return********************************");

                    if (resultCreateBuf.Success)
                    {
                        foreach (var item in model)
                        {
                            await callStoreCommon.CreateQueueCNFRM("PP_RETURN", item.idQueue, item.transactionId, "PP_RETURN", "DONE", "GetPPReturn.txt");
                        }
                        return new ApiResult() { code = "200", message = resultCreateBuf.Message };
                    }
                    else
                    {
                        foreach (var item in model)
                        {
                            log.Error("Fail: " + resultCreateBuf.Message);
                            await callStoreCommon.CreateQueueCNFRM("PP_RETURN", item.idQueue, item.transactionId, "PP_RETURN", "INIT", "GetPPReturn.txt");
                        }
                        return new ApiResult() { code = "500", message = resultCreateBuf.Message };
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
                    return new ApiResult() { code = "500", message = "Không thể kết nối tới máy chủ." };
                else
                    return new ApiResult() { code = "500", message = ex.Message };
            }
        }
    }
}