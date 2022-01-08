using log4net;
using Oracle.ManagedDataAccess.Client;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Sync.Core.Helper
{
    public class CallStoreCommon
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DatabaseOracle databaseOracle;
        public CallStoreCommon(string connectionStr)
        {
            databaseOracle = new DatabaseOracle(connectionStr);
        }

        /// <summary>
        /// Thêm hàng đợi đồng bộ trạng thái lỗi khi lưu buf lỗi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Create_UpdateResponse> CreateBufQueueCNFRM(string _object, decimal? objcectId, string object_Code, string objectType, string status)
        {

            var result = new Create_UpdateResponse();
            try
            {
                List<OracleParameter> lstParam = new List<OracleParameter>
                {
                    new OracleParameter("p_OBJECT", OracleDbType.Varchar2, _object, ParameterDirection.Input),
                    new OracleParameter("p_OBJECT_ID", OracleDbType.Decimal, objcectId, ParameterDirection.Input),
                    new OracleParameter("p_OBJECT_CODE", OracleDbType.Varchar2, object_Code, ParameterDirection.Input),
                    new OracleParameter("p_OBJECT_TYPE", OracleDbType.Varchar2, objectType, ParameterDirection.Input),
                    new OracleParameter("p_STATUS", OracleDbType.Varchar2, status, ParameterDirection.Input),
                    new OracleParameter("p_ERR_CODE", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output),
                    new OracleParameter("p_ERR_MESSAGE", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output)
                };
                var resultProc = await databaseOracle.ExecuteProcNonQuery("EPA_COMFIRM_SYNC_NPC.INSERT_SYNC_QUEUE_CONFIRM", lstParam);
                var p_ERR_CODE = lstParam[lstParam.Count - 2].Value.ToString();
                var p_ERR_MESSAGE = lstParam[lstParam.Count - 1].Value.ToString();
                if (resultProc == -1 && p_ERR_CODE == "OK")
                {
                    result.success = true;
                    result.message = "Thêm mới hàng đợi thành công!";
                }
                else
                {
                    result.success = false;
                    result.message = "Thêm mới hàng đợi không thành công!. Lỗi: " + p_ERR_MESSAGE;
                }
            }
            catch (Exception ex)
            {
                result.ConnectDB = databaseOracle.CheckConnect();
                result.success = false;
                result.message = "Thêm mới hàng đợi không thành công!. Lỗi: " + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Thêm hàng đợi xác nhận đồng bộ về thành công hay lỗi
        /// </summary>
        /// <param name="_object"></param>
        /// <param name="objcectId"></param>
        /// <param name="object_Code"></param>
        /// <param name="objectType"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<Create_UpdateResponse> CreateQueueCNFRM(string _object, decimal? objcectId, string object_Code, string objectType, string status, string fileName)
        {
            var result = new Create_UpdateResponse();
            result = await CreateBufQueueCNFRM(_object, objcectId, object_Code, objectType, status);
            // Trường hợp thêm hàng đợi xác nhận xuống db không thành công thì sẽ ghi lại vào file
            if (result.success == false)
            {
                string path = Environment.CurrentDirectory + "\\Files\\" + fileName;
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                string stringData = objcectId + "," + object_Code + "," + objectType + "," + status;
                File.AppendAllText(path, stringData + Environment.NewLine);
            }
            return result;
        }


        /// <summary>
        /// Thêm hàng đợi đồng bộ trạng thái lỗi khi lưu buf lỗi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Create_UpdateResponse> UpdateStatusQueuePro(decimal? id, string status)
        {
            var result = new Create_UpdateResponse() { ConnectDB = true };
            try
            {
                List<OracleParameter> lstParam = new List<OracleParameter>
                {
                    new OracleParameter("p_ID", OracleDbType.Decimal, id, ParameterDirection.Input),
                    new OracleParameter("p_STATUS", OracleDbType.Varchar2, status, ParameterDirection.Input),
                    new OracleParameter("p_ERR_CODE", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output),
                    new OracleParameter("p_ERR_MESSAGE", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output)
                };
                var resultProc = await databaseOracle.ExecuteProcNonQuery("EPA_COMFIRM_SYNC_NPC.PRC_UPDATE_SYNC_STATUS", lstParam);
                var p_ERR_CODE = lstParam[lstParam.Count - 2].Value.ToString();
                var p_ERR_MESSAGE = lstParam[lstParam.Count - 1].Value.ToString();
                if (resultProc == -1 && p_ERR_CODE == "OK")
                {
                    result.success = true;
                    result.message = "Thêm mới hàng đợi thành công!";
                }
                else
                {
                    result.success = false;
                    result.message = "Thêm mới hàng đợi không thành công!. Lỗi: " + p_ERR_MESSAGE;
                }
            }
            catch (Exception ex)
            {
                result.ConnectDB = databaseOracle.CheckConnect();
                result.success = false;
                result.message = "Thêm mới hàng đợi không thành công!. Lỗi: " + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Thêm hàng đợi xác nhận đồng bộ về thành công hay lỗi
        /// </summary>
        /// <param name="_object"></param>
        /// <param name="id"></param>
        /// <param name="object_Code"></param>
        /// <param name="objectType"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<Create_UpdateResponse> UpdateStatusQueue(decimal? id, string status, string fileName, string connectionStr)
        {
            var result = new Create_UpdateResponse();
            result = await UpdateStatusQueuePro(id, status);
            // Trường hợp thêm hàng đợi xác nhận xuống db không thành công thì sẽ ghi lại vào file
            if (result.success == false)
            {
                try
                {
                    string path = Environment.CurrentDirectory + "\\Files\\" + fileName;
                    if (!File.Exists(path))
                    {
                        File.Create(path).Close();
                    }
                    string stringData = id + "," + status;
                    File.AppendAllText(path, stringData + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    log.Error("Ghi file " + fileName + " lỗi: " + ex.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// Đọc dữ liệu từ file update or insert vào hàng đợi
        /// </summary>
        /// <param name="isPostData">
        /// true: update hàng đợi chờ đồng bộ dữ liệu lên TTĐH
        /// false: insert hàng đợi cập nhật trạng thái đồng bộ dữ liệu về địa phương
        /// </param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<Create_UpdateResponse> ReadFile_UpdateStatusQueue(bool isPostData, string fileName)
        {
            var result = new Create_UpdateResponse() { ConnectDB = true };
            // Trường hợp thêm hàng đợi xác nhận xuống db không thành công thì sẽ ghi lại vào file

            string path = HostingEnvironment.MapPath($"/{fileName}");
            if (!File.Exists(path))
                File.Create(path).Dispose();

            if (File.Exists(path))
            {
                List<string> listError = new List<string>();
                string[] lines = System.IO.File.ReadAllLines(path);
                if (lines != null && lines.Length > 0)
                {
                    if (isPostData)
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            var arrayString = lines[i].Split(',');
                            var resultPro = await UpdateStatusQueuePro(Convert.ToDecimal(arrayString[0]), arrayString[1]);
                            if (resultPro.success == false)
                            {
                                listError.Add(lines[i]);
                                result = resultPro;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            var arrayString = lines[i].Split(',');
                            var resultPro = await CreateBufQueueCNFRM(arrayString[2], Convert.ToDecimal(arrayString[0]), arrayString[1], arrayString[2], arrayString[3]);
                            if (resultPro.success == false)
                            {
                                listError.Add(lines[i]);
                                result = resultPro;
                            }
                        }
                    }
                }

                File.WriteAllText(path, String.Empty);
                if (listError != null && listError.Count > 0)
                {
                    for (int i = 0; i < listError.Count; i++)
                    {
                        File.AppendAllText(path, listError[i] + Environment.NewLine);
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// Ghi lại log khi truyền nhận dữ liệu
        /// </summary>
        /// <param name="code">Mã của thông tin truyền lên</param>
        /// <param name="type">
        /// /// <param name="type">Loại dữ liệu đồng bộ. vd: 
        /// Du lieu ho so = 'DOC';
        /// Du lieu ds A = 'A_LIST';
        /// Du lieu BUF = 'BUF'; 
        /// Tt ca nhan tom luoc = 'PERSON'; 
        /// Tt ca nhan day du = 'PERSON_FULL'; 
        /// Khop thong tin ca nhan = 'KHOPCN';
        /// Kq tra ho chieu = 'ISSUANCE';
        /// Da nhan ho chieu = 'GPP_STT';
        /// Them moi thong tin ho so = 'DOC';
        /// Cap nhat tt ho so = 'DOC_UPD';
        /// Danh sach B = 'B_LIST';
        /// Danh sach C = 'C_LIST';
        /// Them moi thong tin ho chieu = 'PASSPORT';
        /// Cap nhat thong tin ho chieu = 'PASSPORT_UPD';
        /// Them moi ho so day du = 'DOC_FULL';
        /// </param>
        /// <param name="json">Json truyền lên api hoặc json Api trả lại</param>
        /// <param name="message">nội dung lỗi</param>
        /// <param name="codeResultAPI">Mã lỗi bên Api trả lại</param>
        /// <returns></returns>
        public async Task<Create_UpdateResponse> CreateLogSync(string code, string type, string json, string message, string codeResultAPI)
        {
            var result = new Create_UpdateResponse() { ConnectDB = true };
            try
            {
                List<OracleParameter> lstParam = new List<OracleParameter>
                {
                    new OracleParameter("p_TRANS_TYPE", OracleDbType.Varchar2, type, ParameterDirection.Input),
                    new OracleParameter("p_TRANS_RESULT", OracleDbType.Varchar2, codeResultAPI, ParameterDirection.Input),
                    new OracleParameter("p_TRANS_DETAIL", OracleDbType.Varchar2, message, ParameterDirection.Input),
                     new OracleParameter("p_TRANS_OBJECT", OracleDbType.Varchar2, code, ParameterDirection.Input),
                    new OracleParameter("p_TRANS_DATA", OracleDbType.Varchar2, json, ParameterDirection.Input),
                    new OracleParameter("p_ERR_CODE", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output),
                    new OracleParameter("p_ERR_MESSAGE", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output)
                };
                var resultProc = await databaseOracle.ExecuteProcNonQuery("EPA_COMFIRM_SYNC_NPC.PRC_CREATE_SYNC_LOG", lstParam);
                var p_ERR_CODE = lstParam[lstParam.Count - 2].Value.ToString();
                var p_ERR_MESSAGE = lstParam[lstParam.Count - 1].Value.ToString();
                if (resultProc == -1 && p_ERR_CODE == "OK")
                {
                    result.success = true;
                    result.message = "Thêm mới log thành công!";
                }
                else
                {
                    result.success = false;
                    result.message = "Thêm mới log không thành công!. Lỗi: " + p_ERR_MESSAGE;
                }
            }
            catch (Exception ex)
            {
                result.ConnectDB = databaseOracle.CheckConnect();
                result.success = false;
                result.message = "Thêm mới log không thành công!. Lỗi: " + ex.Message;
            }
            log.Error("Ghi log - " + code + "-" + result.message);
            return result;
        }
    }
}

