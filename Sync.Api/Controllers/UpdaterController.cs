using Ionic.Zip;
using log4net;
using Microsoft.Web.Administration;
using Newtonsoft.Json;
using Sync.Api.Models.Updater;
using Sync.Api.Utils;
using Sync.Core.Helper;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
//using System.Configuration;

namespace Sync.Api.Controllers
{
    public class UpdaterController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string WEBA_TYPE = "WEBA";
        private const string TRANS_TYPE = "TRANS";
        private const string RECEI_TYPE = "RECEI";
        private const string SCRPT_TYPE = "SCRPT";

        //private const string WEB_APP_TYPE = "WEB_APP_TYPE";
        //private const string WINF_APP_TYPE = "WINF_APP_TYPE";
        //private const string SCRPT_APP_TYPE = "SCRPT_APP_TYPE";

        // TODO correct Name
        private const string TRANS_NAME_APP = "EPP_SYNC_PUSH.exe";
        private const string RECEI_NAME_APP = "EPP_SYNC_GET.exe";

        // Note path has \ at the end
        // Path Run APP
        private static string WEBA_PATH_RUN_APP = ConfigurationSettings.AppSettings.Get("EPP_WEB_PATH_RUN_APP");
        private static string TRANS_PATH_RUN_APP = ConfigurationSettings.AppSettings.Get("EPP_SYNC_PUSH_PATH_RUN_APP");
        private static string RECEI_PATH_RUN_APP = ConfigurationSettings.AppSettings.Get("EPP_SYNC_GET_PATH_RUN_APP");
        private static string SCRPT_PATH_RUN_APP = ConfigurationSettings.AppSettings.Get("DB_SCRIPTS_PATH_RUN_APP");
        
        private static string APP_NAME_EPP_UPDATE = ConfigurationSettings.AppSettings.Get("APP_NAME_EPP_UPDATE");
        private static string APP_NAME_EPP_SYNC_PUSH_UPDATE = ConfigurationSettings.AppSettings.Get("APP_NAME_EPP_SYNC_PUSH_UPDATE");
        private static string APP_NAME_EPP_SYNC_GET_UPDATE = ConfigurationSettings.AppSettings.Get("APP_NAME_EPP_SYNC_GET_UPDATE");
        private static string APP_NAME_DB_SCRIPTS_UPDATE = ConfigurationSettings.AppSettings.Get("APP_NAME_DB_SCRIPTS_UPDATE");
        
        private static string DIR_NAME_EPP_UPDATE = ConfigurationSettings.AppSettings.Get("DIR_NAME_EPP_UPDATE");
        private static string DIR_NAME_EPP_SYNC_PUSH_UPDATE = ConfigurationSettings.AppSettings.Get("DIR_NAME_EPP_SYNC_PUSH_UPDATE");
        private static string DIR_NAME_EPP_SYNC_GET_UPDATE = ConfigurationSettings.AppSettings.Get("DIR_NAME_EPP_SYNC_GET_UPDATE");
        private static string DIR_NAME_DB_SCRIPTS_UPDATE = ConfigurationSettings.AppSettings.Get("DIR_NAME_DB_SCRIPTS_UPDATE");

        // Path update
        private string WEBA_PATH_UPDATE = WEBA_PATH_RUN_APP + DIR_NAME_EPP_UPDATE + "\\";
        private string TRANS_PATH_UPDATE = TRANS_PATH_RUN_APP + DIR_NAME_EPP_SYNC_PUSH_UPDATE + "\\";
        private string RECEI_PATH_UPDATE = RECEI_PATH_RUN_APP + DIR_NAME_EPP_SYNC_GET_UPDATE + "\\";
        private string SCRPT_PATH_UPDATE = SCRPT_PATH_RUN_APP + DIR_NAME_DB_SCRIPTS_UPDATE + "\\";
        
        private string BACKUP_DIR = "BACKUP_DIR";

        // GET: Updater
        public async Task<IHttpActionResult> Index()
        {
            return Ok(new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công" });
        }

        /// <summary>
        /// Each time call this service will create new update
        /// therefor Delete all files uploaded
        /// Support ZIP files and Split of ZIP file
        /// </summary>
        /// <param name="startUpdateInput"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Updater(StartUpdateInput startUpdateInput)
        {
            try
            {
                if (startUpdateInput == null)
                    throw new Exception("Dữ liệu đầu vào bị null");

                if (!WEBA_TYPE.Equals(startUpdateInput.type) && !TRANS_TYPE.Equals(startUpdateInput.type)
                    && !RECEI_TYPE.Equals(startUpdateInput.type) && !SCRPT_TYPE.Equals(startUpdateInput.type))
                {
                    return Ok(new ApiResult() { code = ((int)HttpStatusCode.NotAcceptable).ToString(), message = "Thất bại: Không tồn tại type này" });
                }

                if (startUpdateInput.listId == null || !startUpdateInput.listId.Any())
                {
                    return Ok(new ApiResult() { code = ((int)HttpStatusCode.NotAcceptable).ToString(), message = "Thất bại: Không tìm thấy danh sách files" });
                }

                if (startUpdateInput.total == 0)
                {
                    return Ok(new ApiResult() { code = ((int)HttpStatusCode.NotAcceptable).ToString(), message = "Thất bại: Tổng số files phải lớn hơn không" });
                }

                if (startUpdateInput.total != startUpdateInput.listId.Count)
                {
                    return Ok(new ApiResult() { code = ((int)HttpStatusCode.NotAcceptable).ToString(), message = "Thất bại: Tổng số files và số files trong danh sách không khớp" });
                }

                string sessionId = DateTime.Now.ToString("yyMMddHHmmss");

                switch (startUpdateInput.type)
                {
                    case WEBA_TYPE:
                        DataGroup webAGroup = new DataGroup();
                        webAGroup.type = WEBA_TYPE;
                        webAGroup.total = startUpdateInput.total;

                        if (!Directory.Exists(WEBA_PATH_UPDATE))
                        {
                            // Check if not Exist WEBA_PATH_UPDATE then create it
                            Directory.CreateDirectory(WEBA_PATH_UPDATE);
                        } else
                        {
                            //Delete all file in this Directory
                            DeleteAllFileInDir(WEBA_PATH_UPDATE);
                        }

                        // Created Json file save meta data
                        File.WriteAllText(WEBA_PATH_UPDATE + "\\" + sessionId + ".json", JsonConvert.SerializeObject(startUpdateInput));

                        break;
                    case TRANS_TYPE:
                        DataGroup transGroup = new DataGroup();
                        transGroup.type = TRANS_TYPE;
                        transGroup.total = startUpdateInput.total;

                        if (!Directory.Exists(TRANS_PATH_UPDATE))
                        {
                            // Check if not Exist TRANS_PATH_UPDATE then create it
                            Directory.CreateDirectory(TRANS_PATH_UPDATE);
                        }
                        else
                        {
                            //Delete all file in this Directory
                            DeleteAllFileInDir(TRANS_PATH_UPDATE);
                        }

                        // Created Json file save meta data
                        File.WriteAllText(TRANS_PATH_UPDATE + "\\" + sessionId + ".json", JsonConvert.SerializeObject(startUpdateInput));

                        break;
                    case RECEI_TYPE:
                        DataGroup receiveGroup = new DataGroup();
                        receiveGroup.type = RECEI_TYPE;
                        receiveGroup.total = startUpdateInput.total;

                        if (!Directory.Exists(RECEI_PATH_UPDATE))
                        {
                            // Check if not Exist RECEI_PATH_UPDATE then create it
                            Directory.CreateDirectory(RECEI_PATH_UPDATE);
                        }
                        else
                        {
                            //Delete all file in this Directory
                            DeleteAllFileInDir(RECEI_PATH_UPDATE);
                        }

                        // Created Json file save meta data
                        File.WriteAllText(RECEI_PATH_UPDATE + "\\" + sessionId + ".json", JsonConvert.SerializeObject(startUpdateInput));

                        break;
                    case SCRPT_TYPE:
                        DataGroup scriptGroup = new DataGroup();
                        scriptGroup.type = SCRPT_TYPE;
                        scriptGroup.total = startUpdateInput.total;

                        if (!Directory.Exists(SCRPT_PATH_UPDATE))
                        {
                            // Check if not Exist SCRPT_PATH_UPDATE then create it
                            Directory.CreateDirectory(SCRPT_PATH_UPDATE);
                        }
                        else
                        {
                            //Delete all file in this Directory
                            DeleteAllFileInDir(SCRPT_PATH_UPDATE);
                        }

                        // Created Json file save meta data
                        File.WriteAllText(SCRPT_PATH_UPDATE + "\\" + sessionId + ".json", JsonConvert.SerializeObject(startUpdateInput));

                        break;
                }

                return Ok(new ApiResultUpdate() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công", key = sessionId });
            }
            catch (Exception ex)
            {

                return Ok(new ApiResult() { code = ((int)HttpStatusCode.NotAcceptable).ToString(), message = "Thất bại: " + ex.Message });
            }
        }

        private void DeleteAllFileInDir(string dirPath)
        {
            if (string.IsNullOrWhiteSpace(dirPath))
                return;

            var directInfo = new DirectoryInfo(dirPath);
            var files = directInfo.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (var f in files)
            {
                try
                {
                    f.Delete();
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Get each zip files
        /// </summary>
        /// <param name="partFile"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> DownloadPartFile (PartFile partFile)
        {
            try
            {
                if (partFile == null)
                    throw new Exception("Dữ liệu đầu vào bị null");
                if (string.IsNullOrWhiteSpace(partFile.fileName))
                    throw new Exception("File name is null or empty!");
                if (partFile.fileName.Contains(".exe") || partFile.fileName.Contains(".bat") || partFile.fileName.Contains(".reg") || partFile.fileName.Contains(".sh"))
                    throw new Exception("File name có chứa thành phần nguy hiểm!");
                if (string.IsNullOrWhiteSpace(partFile.key))
                    throw new Exception("Key cannot be null or empty!");
                if (string.IsNullOrWhiteSpace(partFile.type))
                    throw new Exception("Type cannot be null or empty!");
                if (string.IsNullOrWhiteSpace(partFile.data))
                    throw new Exception("Data cannot be null or empty!");

                switch (partFile.type)
                {
                    case WEBA_TYPE:
                        File.WriteAllBytes(WEBA_PATH_UPDATE + "\\" + partFile.fileName, Convert.FromBase64String(partFile.data));

                        break;
                    case TRANS_TYPE:
                        // Check Existed PartFile
                        File.WriteAllBytes(TRANS_PATH_UPDATE + "\\" + partFile.fileName, Convert.FromBase64String(partFile.data));
                        
                        break;
                    case RECEI_TYPE:
                        File.WriteAllBytes(RECEI_PATH_UPDATE + "\\" + partFile.fileName, Convert.FromBase64String(partFile.data));
                        
                        break;
                    case SCRPT_TYPE:
                        File.WriteAllBytes(SCRPT_PATH_UPDATE + "\\" + partFile.fileName, Convert.FromBase64String(partFile.data));
                        
                        break;
                }

                return Ok(new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công" });
            }
            catch (Exception ex)
            {

                return Ok(new ApiResult() { code = ((int)HttpStatusCode.NotAcceptable).ToString(), message = "Thất bại: " + ex.Message });
            }
        }

        public async Task<IHttpActionResult> UploadFinished(UpdateFinishInput updateFinishInput)
        {
            try
            {
                if (updateFinishInput == null || string.IsNullOrWhiteSpace(updateFinishInput.key))
                    throw new Exception("Dữ liệu đầu vào bị null hoặc key không phù hợp!");

                switch (updateFinishInput.type)
                {
                    case WEBA_TYPE:
                        ProcessAfterDownload(WEBA_PATH_UPDATE, WEBA_PATH_RUN_APP, WEBA_TYPE, DIR_NAME_EPP_UPDATE, APP_NAME_EPP_UPDATE, updateFinishInput.key, false);
                        break;

                    case TRANS_TYPE:
                        ProcessAfterDownload(TRANS_PATH_UPDATE, TRANS_PATH_RUN_APP, TRANS_TYPE, DIR_NAME_EPP_SYNC_PUSH_UPDATE, APP_NAME_EPP_SYNC_PUSH_UPDATE, updateFinishInput.key, false);
                        break;

                    case RECEI_TYPE:
                        ProcessAfterDownload(RECEI_PATH_UPDATE, RECEI_PATH_RUN_APP, RECEI_TYPE, DIR_NAME_EPP_SYNC_GET_UPDATE, APP_NAME_EPP_SYNC_GET_UPDATE, updateFinishInput.key, false);
                        break;

                    case SCRPT_TYPE:
                        ProcessAfterDownload(SCRPT_PATH_UPDATE, SCRPT_PATH_RUN_APP, SCRPT_TYPE, DIR_NAME_DB_SCRIPTS_UPDATE, APP_NAME_DB_SCRIPTS_UPDATE, updateFinishInput.key, true);
                        break;
                }

                return Ok(new ApiResult() { code = ((int)HttpStatusCode.OK).ToString(), message = "Thành công" });
            }
            catch (Exception ex)
            {

                return Ok(new ApiResult() { code = ((int)HttpStatusCode.NotAcceptable).ToString(), message = "Thất bại: " + ex.Message });
            }
        }

        private void ProcessAfterDownload(string workPath, string runAppPath, string typeApp, string updateDirName, string appName, string sessiongId, bool isScripDb = false)
        {
            // Check list files name and list files physical saved 
            try
            {
                CheckListFiles(workPath, sessiongId);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi liên quan file cấu hình cập nhật, Cần đẩy lại từ đầu! " + ex.Message);
            }

            // Process extract one or more file zip
            // Stop service of each type
            // Copy to each folder type
            // Start service 
            // Clear list, (Delete backup files)

            string appPath = System.Environment.CurrentDirectory;
            
            string fileName = "DefaultApp";

            var directoryFileZip = new DirectoryInfo(workPath);
            var fileZips = directoryFileZip.GetFiles("*.zip", SearchOption.AllDirectories);

            if(fileZips.Length > 1)
            {
                throw new Exception("Có nhiều hơn 01 file .zip trong thư mục cập nhật");
            } else if (fileZips.Length < 1)
            {
                throw new Exception("Không tồn tại file .zip trong thư mục cập nhật");
            }

            // Extract
            foreach (FileInfo f in fileZips)
            {
                if (f.FullName.Contains(".zip"))
                {
                    string fullFilePath = directoryFileZip.FullName;
                    if (!fullFilePath.EndsWith("\\"))
                    {
                        fullFilePath = directoryFileZip.FullName + @"\";
                    }

                    string destination = f.FullName.Replace(fullFilePath, "");
                    fileName = destination;
                    break;
                }
            }
            var zipfile = string.Empty;
            try
            {
                zipfile = Path.Combine(workPath, fileName);
                using (var zip = Ionic.Zip.ZipFile.Read(zipfile))
                {
                    string outputPath = Environment.CurrentDirectory;

                    zip.ExtractAll(workPath, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể giải nén file update: " + ex.Message);
            }

            string strPathTarget = "";
            if (isScripDb)
            {
                var fileInfo = new FileInfo(zipfile);
                var dirInfo = new DirectoryInfo(fileInfo.DirectoryName);
                var files = dirInfo.GetFiles("*.sql", SearchOption.AllDirectories);

                foreach (var f in files)
                {
                    RunTheScriptFile(f.FullName);
                }
            } else
            {
                try
                {
                    // Backup to BACKUP_DIR
                    var backupPath = Path.Combine(runAppPath, BACKUP_DIR);
                    if (!Directory.Exists(backupPath))
                        Directory.CreateDirectory(backupPath);

                    var directory = new DirectoryInfo(runAppPath);
                    var files = directory.GetFiles("*.*", SearchOption.AllDirectories);
                    foreach (FileInfo f in files)
                    {
                        strPathTarget = "";

                        if (!f.FullName.EndsWith(".tmp") && !f.FullName.Contains(BACKUP_DIR) && !f.FullName.Contains(updateDirName))
                        {
                            string destination = f.Name;

                            string fullDirPath = directory.FullName;
                            if (!fullDirPath.EndsWith("\\"))
                            {
                                fullDirPath = directory.FullName + @"\";
                            }
                            string destinationSubPath = f.FullName.Replace(fullDirPath, "");

                            strPathTarget = Path.Combine(backupPath, destinationSubPath);

                            var fileInfo = new FileInfo(strPathTarget);
                            if (!Directory.Exists(fileInfo.DirectoryName))
                                Directory.CreateDirectory(fileInfo.DirectoryName);

                            f.CopyTo(strPathTarget, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Sao lưu file lỗi! " + ex.Message);
                }

                // Copy from directory extracted to run app path
                string strPathExtracted = Path.Combine(workPath, zipfile.Replace(".zip", string.Empty));
                var extractedDir = new DirectoryInfo(strPathExtracted);
                var fileInfoExtracteds = extractedDir.GetFiles("*.*", SearchOption.AllDirectories);
                
                foreach (FileInfo f in fileInfoExtracteds)
                {
                    try
                    {
                        strPathTarget = "";
                        string destination = f.Name;

                        string fullDirPath = extractedDir.FullName;
                        if (!fullDirPath.EndsWith("\\"))
                        {
                            fullDirPath = extractedDir.FullName + @"\";
                        }
                        string destinationSubPath = f.FullName.Replace(fullDirPath, "");

                        strPathTarget = Path.Combine(runAppPath, destinationSubPath);

                        var fileInfo = new FileInfo(strPathTarget);
                        if (!Directory.Exists(fileInfo.DirectoryName))
                            Directory.CreateDirectory(fileInfo.DirectoryName);

                        f.CopyTo(strPathTarget, true);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Update Copy file đến thư mục đích lỗi, cần đẩy lại file .zip và gọi lại services finish!" + ex.Message);
                    }
                }
            }

            // Delete Backup
            try
            {
                var strBackupPath = Path.Combine(runAppPath, BACKUP_DIR);
                var directoryInfoBk = new DirectoryInfo(strBackupPath);
                var backupFiles = directoryInfoBk.GetFiles("*.*", SearchOption.AllDirectories);

                foreach (FileInfo f in backupFiles)
                {
                    try
                    {
                        f.Delete();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                // Delete folder
                directoryInfoBk.Delete(true);
            }
            catch (Exception ex)
            {
                //throw new Exception("Xóa bỏ thư mục backup lỗi!" + ex.Message);
            }

            // Call restart site
            // Last clear container
            try
            {
                RestartAndClear(appPath, typeApp, appName);
            }
            catch (Exception ex)
            {
                throw new Exception("Khởi động lại ứng dụng lỗi, Cần khởi động lại ứng dụng bằng tay!" + ex.Message);
            }

            //Environment.Exit(0);
        }

        private void CheckListFiles(string workPath, string sessiongId)
        {
            var jsonFileName = sessiongId + ".json";
            var jsonPath = Path.Combine(workPath, jsonFileName);
            var jsonOrg = File.ReadAllText(jsonPath);
            var startUpdateInput = JsonConvert.DeserializeObject<StartUpdateInput>(jsonOrg);

            var directInfo = new DirectoryInfo(workPath);
            var fileTops = directInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);

            if ((fileTops.Length - 1) != startUpdateInput.listId.Count || startUpdateInput.listId.Count != startUpdateInput.total)
                throw new Exception("Số lượng files đẩy lên và lưu được đang khác nhau! ");

            bool isExist = false;
            var partFileName = string.Empty;
            foreach (var f in fileTops)
            {
                isExist = false;
                if (!f.FullName.EndsWith(".json"))
                {
                    foreach (var item in startUpdateInput.listId)
                    {
                        if (f.Name.Equals(item.fileName))
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        partFileName = f.Name;
                        break;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(partFileName))
            {
                throw new Exception("Danh sách File đẩy lên khác danh sách file đăng ký đẩy! file name: " + partFileName);
            }
        }
        private void RestartAndClear(string appPath, string typeApp, string appName)
        {
            try
            {
                string temp = AppDomain.CurrentDomain.BaseDirectory;
                var processInfo = new ProcessStartInfo(temp + @"Batch\RestartApp.bat", appName);
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;

                var process = new Process();
                process.StartInfo = processInfo;
                process.Start();

                process.WaitForExit();

                logger.Info("ExitCode: " + process.ExitCode);
                process.Close();
            }
            catch (Exception ex)
            {
                logger.Error("Exception!, ex = " + ex.Message);
                throw new InvalidOperationException("Có lỗi xảy ra trong quá trình khởi động lại ứng dụng, cần khởi động lại ứng dụng bằng tay");
            }


        }

        private void MergeExtract(List<ZipArchive> archives, string outputPath)
        {
            if (archives == null) throw new ArgumentNullException("archives");
            //if (archives.Count == 1) return archives.Single();

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var zipArchive in archives)
                    {
                        foreach (var zipArchiveEntry in zipArchive.Entries)
                        {
                            var file = archive.CreateEntry(zipArchiveEntry.FullName);

                            using (var entryStream = file.Open())
                            {
                                using (var streamWriter = new StreamWriter(entryStream)) 
                                { 
                                    streamWriter.Write(zipArchiveEntry.Open()); 
                                }
                            }
                        }
                    }

                    archive.ExtractToDirectory(outputPath);
                    
                }
            }
        }
         
        private ZipArchive Merge(List<ZipArchive> archives)
        {
            if (archives == null) throw new ArgumentNullException("archives");
            if (archives.Count == 1) return archives.Single();

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var zipArchive in archives)
                    {
                        foreach (var zipArchiveEntry in zipArchive.Entries)
                        {
                            var file = archive.CreateEntry(zipArchiveEntry.FullName);

                            using (var entryStream = file.Open())
                            {
                                using (var streamWriter = new StreamWriter(entryStream)) { streamWriter.Write(zipArchiveEntry.Open()); }
                            }
                        }
                    }

                    return archive;
                }
            }
        }
        //private void ExtractZipFile(string inPath, string outPath)
        //{
        //    string zipPath = inPath;

        //    string extractPath = outPath;

        //    // Normalizes the path.
        //    extractPath = Path.GetFullPath(extractPath);

        //    // Ensures that the last character on the extraction path
        //    // is the directory separator char.
        //    // Without this, a malicious zip file could try to traverse outside of the expected
        //    // extraction path.
        //    if (!extractPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
        //        extractPath += Path.DirectorySeparatorChar;

        //    using (ZipArchive archive = ZipFile.OpenRead(zipPath))
        //    {
        //        foreach (ZipArchiveEntry entry in archive.Entries)
        //        {
        //            if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
        //            {
        //                // Gets the full path to ensure that relative segments are removed.
        //                string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));

        //                // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
        //                // are case-insensitive.
        //                if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
        //                    entry.ExtractToFile(destinationPath);
        //            }
        //        }
        //    }
        //}

        // Chạy file script thay đổi DB
        private void RunTheScriptFile(string filePath)
        {
            try
            {
                string content = File.ReadAllText(filePath);
                // Đang lấy connection đến Perso, sau khi chỉnh sửa thành 1 ConnectionString thì sửa lại
                var rs = databaseOracle.ExecuteNonQuery(content);
            }
            catch (Exception ex)
            {

                throw new Exception("Có lỗi xảy ra khi chạy file script." + ex.Message);
            }
        }

    }
}