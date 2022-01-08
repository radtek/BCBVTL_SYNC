using Newtonsoft.Json;
using Sync.Core.Models;
using Sync.Web.Models;
using Sync.Web.ScheduleTasks;
using Sync.Web.Services;
using Sync.Web.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sync.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly ProcessService processSrv = new ProcessService();
        readonly string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Start/ProcessConfig.json");
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetListProcess()
        {
            var model = processSrv.GetListProcess();
            return Json(new ResponseList<ProcessModel> { code = "200", data = model }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpdateProcess(ProcessModel model)
        {
            try
            {
                var listProcess = processSrv.GetListProcess();
                var process = listProcess.FirstOrDefault(x => x.Code == model.Code);
                if (process == null)
                    throw new Exception("Lỗi file config!");

                if (model.TimeLoop != process.TimeLoop)
                {
                    _ = JobScheduleChangeTimeloop.ChangeTimeloopAsync(model);
                }

                if (model.Active != process.Active)
                {
                    JobScheduleSingle.StartSingle(model);
                }
                listProcess[listProcess.IndexOf(process)] = model;

                System.IO.File.WriteAllText(configPath, JsonConvert.SerializeObject(listProcess));
                return Json(new ResponseList<ProcessModel> { code = ((int)HttpStatusCode.OK).ToString(), data = listProcess }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ApiResult { code = ((int)HttpStatusCode.InternalServerError).ToString(), message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ShowConfig(string name)
        {
            var cErr = 0;
            while (true)
            {
                if (cErr == 5) { return Json(new ApiResult() { message = "Lỗi!", code = "500" }); }
                try
                {
                    string filePath = Directory.GetCurrentDirectory() + "\\Log\\" + name + $"_{DateTime.Now:yyyyMMdd}" + ".log";
                    return Json(new ApiResult() { message = StringUtils.ReadNLineOfFile(filePath, 200), code = "200" });
                }
                catch (Exception)
                {
                    cErr++;
                    throw;
                }
            }
        }


        public ActionResult GetTypesData()
        {
            var loaiTrungTam = ConfigurationManager.AppSettings.Get("LoaiTrungTam");
            List<TypeData> typeDatas = new List<TypeData>();
            switch (loaiTrungTam)
            {
                case "PA":
                    typeDatas.Add(new TypeData("Gửi hồ sơ lên TTĐH"));
                    typeDatas.Add(new TypeData("Gửi DS đề xuất lên A08"));
                    typeDatas.Add(new TypeData("Gửi KQ trả HC lên TTĐH"));
                    typeDatas.Add(new TypeData("Gửi trạng thái nhận HC"));
                    typeDatas.Add(new TypeData("Cập nhật thông tin hồ sơ lên TTĐH"));
                    break;
                case "A":
                    typeDatas.Add(new TypeData("Gửi DS A lên TTĐH"));
                    typeDatas.Add(new TypeData("Gửi thông tin hồ sơ lên TTĐH"));
                    typeDatas.Add(new TypeData("Gửi thông tin hồ sơ đầy đủ lên TTĐH"));
                    typeDatas.Add(new TypeData("Gửi thông tin hộ chiếu lên TTĐH"));
                    break;
            }

            return Json(new ResponseList<TypeData> { code = "200", data = typeDatas }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetResendDataAsync(string taskName)
        {
            List<ReSendDataModel> reSendDatas = new List<ReSendDataModel>();
            var loaiTrungTam = ConfigurationManager.AppSettings.Get("LoaiTrungTam");
            switch (loaiTrungTam)
            {
                case "PA":
                    if (taskName == "Gửi hồ sơ lên TTĐH")
                    {
                        reSendDatas = await BaseServices.GetDataToResend("DOC");
                    }
                    else if (taskName == "Gửi DS đề xuất lên A08")
                    {
                        reSendDatas = await BaseServices.GetDataToResend("A_LIST");
                    }
                    else if (taskName == "Gửi KQ trả HC lên TTĐH")
                    {
                        reSendDatas = await BaseServices.GetDataToResend("ISSUANCE");
                    }
                    else if (taskName == "Gửi trạng thái nhận HC")
                    {
                        reSendDatas = await BaseServices.GetDataToResend("GPP_STT");
                    }
                    else
                    {
                        reSendDatas = await BaseServices.GetDataToResend("DOC_UPD");
                    }
                    break;
                case "A":
                    if (taskName == "Gửi DS A lên TTĐH")
                    {
                        reSendDatas = await BaseServices.GetDataToResend("A_LIST");
                    }
                    else if (taskName == "Gửi thông tin hồ sơ đầy đủ lên TTĐH")
                    {
                        reSendDatas = await BaseServices.GetDataToResend("DOC_FULL");
                    }
                    else if (taskName == "Gửi thông tin hồ sơ lên TTĐH")
                    {
                        reSendDatas = await BaseServices.GetDataToResend("DOC");
                    }
                    else
                    {
                        reSendDatas = await BaseServices.GetDataToResend("PASSPORT_UPD");
                    }
                    break;
            }

            return Json(new ResponseList<ReSendDataModel> { code = "200", data = reSendDatas }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ResendDataAsync(List<ReSendDataModel> dsHoSo)
        {
            ApiResult apiResult = new ApiResult();
            string listQueueId = string.Join(",", dsHoSo.Where(x => x.Id.HasValue).Select(x => x.Id).ToArray());

            if (listQueueId != "")
            {

                string result = await BaseServices.UpdateQueueToResendData(listQueueId);

                if (string.IsNullOrEmpty(result))
                    apiResult.message = "Cập nhật thành công";
                else
                    apiResult.message = "Cập nhật không thành công";
            }

            return Json(apiResult);
        }
        public ActionResult GetKhuVuc()
        {
            var khuVuc = ConfigurationManager.AppSettings["KhuVuc"];
            return Json(new Response<string> { code = "200", data = khuVuc }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeKhuVuc(string khuVuc)
        {
            try
            {
                //ConfigurationManager.AppSettings["KhuVuc"] = khuVuc;
                //Create the object
                Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

                //make changes
                config.AppSettings.Settings["KhuVuc"].Value = khuVuc;

                //save to apply changes
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return Json(new ApiResult { code = "200", message = null }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ApiResult { code = "500", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}