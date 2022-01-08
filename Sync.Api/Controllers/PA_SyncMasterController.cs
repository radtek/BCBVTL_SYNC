using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Sync.Api.CustomAttr;
using Sync.Api.Models;
using Sync.Api.Models.SyncCategory;
using Sync.Core.Helper;
using Sync.Core.Models;
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
    public class PA_SyncMasterController : BaseController
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
                //var listDatas = new ListDatas
                //{
                //    listCountry = new List<CategoryData>(),
                //    listDistrict = new List<DistrictData>(),
                //    listArea = new List<DistrictData>(),
                //    listPurpose = new List<PurposeData>(),
                //    listBordergate = new List<BordergatData>(),
                //    listOffice = new List<OfficeData>(),
                //    listOrganization = new List<OrganizationData>(),
                //    listFeeInfo = new List<FeeInfoData>(),
                //    listEthnic = new List<ReligionData>(),
                //    listReligion = new List<ReligionData>(),
                //    listRelationship = new List<RelationshipData>(),
                //    listIDPlace = new List<IDPlaceData>(),
                //    listTransactionSubType = new List<TransactionSubTypeData>(),
                //    listPassportType = new List<PassportTypeData>(),
                //};
                //if (model.listCountry != null && model.listCountry.Count > 0)
                //    listDatas.listCountry.AddRange(model.listCountry);

                //if (model.listDistrict != null && model.listDistrict.Count > 0)
                //    listDatas.listDistrict.AddRange(model.listDistrict);

                //if (model.listArea != null && model.listArea.Count > 0)
                //    listDatas.listArea.AddRange(model.listArea);

                //if (model.listPurpose != null && model.listPurpose.Count > 0)
                //    listDatas.listPurpose.AddRange(model.listPurpose);

                //if (model.listBordergate != null && model.listBordergate.Count > 0)
                //    listDatas.listBordergate.AddRange(model.listBordergate);

                //if (model.listOffice != null && model.listOffice.Count > 0)
                //    listDatas.listOffice.AddRange(model.listOffice);

                //if (model.listOrganization != null && model.listOrganization.Count > 0)
                //    listDatas.listOrganization.AddRange(model.listOrganization);

                //if (model.listFeeInfo != null && model.listFeeInfo.Count > 0)
                //    listDatas.listFeeInfo.AddRange(model.listFeeInfo);

                //if (model.listEthnic != null && model.listEthnic.Count > 0)
                //    listDatas.listEthnic.AddRange(model.listEthnic);

                //if (model.listReligion != null && model.listReligion.Count > 0)
                //    listDatas.listReligion.AddRange(model.listReligion);

                //if (model.listRelationship != null && model.listRelationship.Count > 0)
                //    listDatas.listRelationship.AddRange(model.listRelationship);

                //if (model.listIDPlace != null && model.listIDPlace.Count > 0)
                //    listDatas.listIDPlace.AddRange(model.listIDPlace);

                //if (model.listTransactionSubType != null && model.listTransactionSubType.Count > 0)
                //    listDatas.listTransactionSubType.AddRange(model.listTransactionSubType);

                //if (model.listPassportType != null && model.listPassportType.Count > 0)
                //    listDatas.listPassportType.AddRange(model.listPassportType);

                //var resultCreateBuf = await CreateListCategoriesXml(listDatas);
                var resultCreateBuf = await CreateListCategoriesXml(model);
                string jsonDB = JsonConvert.SerializeObject(resultCreateBuf, Formatting.Indented);
                if (!databaseOracle.CheckConnect())
                {
                    var resultLog1 = await callStoreCommon.CreateLogSync("", "STATUS_A_LIST", jsonDB, "Không có kết nối đến Database", "FAIL_DB");
                    throw new Exception("Không có kết nối đến Database");
                }

                var resultLog = await callStoreCommon.CreateLogSync("", "STATUS_A_LIST", jsonDB, resultCreateBuf.Message, (!string.IsNullOrEmpty(resultCreateBuf.Message) ? "FAIL" : "SUCCESS"));

                return Ok(new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công" });
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return Ok(new ApiResult() { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message });
            }
        }

        private async Task<ConfirmSyncDeleteModel> CreateListCategoriesXml(ListDatas listCategories)
        {
            var result = new ConfirmSyncDeleteModel() { ConnectDB = true };
            try
            {
                log.Info("****************************BEGIN thêm dữ liệu danh mục********************************");
                string xmlData = Common.ConvertObjectToXML<ListDatas>(listCategories);
                log.Info("XML data");
                log.Info(xmlData);
                List<OracleParameter> lstParam = new List<OracleParameter> { };
                lstParam.Add(new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input));
                lstParam.Add(new OracleParameter("pMsg", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output));

                var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.prc_cap_nhat_danh_muc", lstParam);
                log.Info("****************************BEGIN thêm dữ liệu danh mục********************************");
                var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
                if (resultProc == -1 && p_ERR_CODE == "OK")
                {
                    result.Message = "";
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Lỗi cập nhật trạng thái gửi danh sách A lên TTĐH: " + p_ERR_CODE;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Lỗi cập nhật trạng thái gửi danh sách A lên TTĐH: " + ex.Message;
                result.ConnectDB = databaseOracle.CheckConnect();
            }
            return result;
        }
    }
}
