using log4net;
using Newtonsoft.Json;
using Sync.Core.Models;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Core.Helper
{
    public class ApiBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string maTrungTam = ConfigurationManager.AppSettings["MaTrungTam"].ToString();
        private static readonly string Token = EppMaHoa.CommonManager.DecryptRSA(ConfigurationManager.AppSettings["Token"].ToString());//Sync.Core.Helper.MaHoa_GiaiMa.Decrypt(ConfigurationManager.AppSettings["Token"].ToString());
        private static readonly string insideUrlMB = ConfigurationManager.AppSettings["insideUrlMB"].ToString();
        private static readonly string insideUrlMN = ConfigurationManager.AppSettings["insideUrlMN"].ToString();

        public static async Task<string> GetTokenAccess(int officeID, string accessToken)
        {
            try
            {
                if (accessToken == "")
                {
                    JsonAPI getToken = JsonConvert.DeserializeObject<JsonAPI>(await Task.FromResult(GetToken()).Result);
                    if (getToken.Status == "00")
                    {
                        log.Info("Không lấy được thông tin token !");
                        return null;
                    }
                    else
                        accessToken = getToken.Data;
                }
                return accessToken;
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return null;
            }
        }

        public static async Task<string> GetToken()
        {
            try
            {
                string tokenOld = ConfigurationManager.AppSettings["tokenValue"];
                JsonAPI json = new JsonAPI();
                if (string.IsNullOrEmpty(tokenOld))
                {
                    string account = ConfigurationManager.AppSettings["accSyncDataNPC"].ToString();
                    string userName = account.Split('-')[0];
                    string password = account.Split('-')[1];

                    var model = new GetTokenInfo { username = userName, password = password };
                    var inputJson = JsonConvert.SerializeObject(model, Formatting.Indented);
                    var khuVuc = ConfigurationManager.AppSettings["KhuVuc"];
                    string insideUrl = khuVuc == "MB" ? insideUrlMB : insideUrlMN;
                    using (var client = new HttpClient())
                    {
                        //Passing service base url  
                        client.BaseAddress = new Uri(insideUrl);

                        client.DefaultRequestHeaders.Clear();
                        //Define request data format  
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("token", Token);
                        client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                        client.Timeout = TimeSpan.FromSeconds(20);
                        string message = "";
                        HttpContent content = new StringContent(inputJson, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await Task.FromResult(client.PostAsync("/Oauth/Token", content).Result);
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(responseString);

                            if (tokenInfo.code == "200")
                            {
                                // Lưu lại token
                                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                                configuration.AppSettings.Settings["tokenValue"].Value = tokenInfo.response.token;
                                configuration.Save(ConfigurationSaveMode.Full, true);
                                ConfigurationManager.RefreshSection("appSettings");
                                json = new JsonAPI { Status = "01", Message = "OK", Data = tokenInfo.response.token };
                            }
                            else
                            {
                                // Lưu lại token
                                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                                configuration.AppSettings.Settings["tokenValue"].Value = "";
                                configuration.Save(ConfigurationSaveMode.Full, true);
                                ConfigurationManager.RefreshSection("appSettings");
                                json = new JsonAPI { Status = "00", Message = tokenInfo.message };
                            }

                        }
                        else message = response.ReasonPhrase;
                    }
                }
                else
                {
                    json = new JsonAPI { Status = "01", Message = "OK", Data = tokenOld };
                }

                return JsonConvert.SerializeObject(json);
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return JsonConvert.SerializeObject(new JsonAPI { Status = "00", Message = ex.Message });
            }
        }

        public static async Task<HttpResponseMessage> UPPostJsonAsync(string accessToken, string uri, string json)
        {
            try
            {
                var khuVuc = ConfigurationManager.AppSettings["KhuVuc"];
                string insideUrl = khuVuc == "MB" ? insideUrlMB : insideUrlMN;
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(insideUrl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("token", Token);
                    client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                    //client.Timeout = TimeSpan.FromSeconds(20);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await Task.FromResult(client.PostAsync(uri, content).Result);
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.Info("---ex End API");
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotImplemented,
                    Content = new StringContent(JsonConvert.SerializeObject(new ApiResult { message = ex.Message }))
                };
            }
        }

        public static async Task<HttpResponseMessage> PostJsonAsync(string accessToken, string uri, string json)
        {
            HttpResponseMessage response = null;
            try
            {
                var khuVuc = ConfigurationManager.AppSettings["KhuVuc"];
                string insideUrl = khuVuc == "MB" ? insideUrlMB : insideUrlMN;
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(insideUrl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(20);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("token", Token);
                    client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                    response = await client.PostAsync(uri, content);
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotImplemented,
                    Content = new StringContent(JsonConvert.SerializeObject(new ApiResult { message = ex.Message }))
                };
            }
        }
        //TEST
        public static async Task<HttpResponseMessage> ReCallPostJsonAsync(string accessToken, string uri, string json)
        {
            try
            {
                string url = uri;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("token", Token);
                    client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // Gọi lại khi token hết hạn
                        // Lưu lại token
                        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        configuration.AppSettings.Settings["tokenValue"].Value = "";
                        configuration.Save(ConfigurationSaveMode.Full, true);
                        ConfigurationManager.RefreshSection("appSettings");
                        await GetToken();
                        accessToken = ConfigurationManager.AppSettings["tokenValue"];
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("token", Token);
                        client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                        response = await client.PostAsync(client.BaseAddress, content);

                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotImplemented,
                    Content = new StringContent(JsonConvert.SerializeObject(new ApiResult { message = ex.Message }))
                };
            }
        }
        //TEST
        public static async Task<string> GetJsonAsync(string accessToken, string url)
        {
            HttpResponseMessage response = null;
            try
            {
                var khuVuc = ConfigurationManager.AppSettings["KhuVuc"];
                string insideUrl = khuVuc == "MB" ? insideUrlMB : insideUrlMN;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(insideUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("token", Token);
                    client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                    response = await client.GetAsync(url);
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // Gọi lại khi token hết hạn
                        // Lưu lại token
                        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        configuration.AppSettings.Settings["tokenValue"].Value = "";
                        configuration.Save(ConfigurationSaveMode.Full, true);
                        ConfigurationManager.RefreshSection("appSettings");
                        await GetToken();
                        accessToken = ConfigurationManager.AppSettings["tokenValue"];
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("token", Token);
                        client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                        response = await client.GetAsync(url);

                    }
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        return responseString;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return ex.Message;
            }
            return null;
        }

        public static async Task<HttpResponseMessage> GetJsonAsyncResponse(string accessToken, string url)
        {
            HttpResponseMessage response = null;
            try
            {
                var khuVuc = ConfigurationManager.AppSettings["KhuVuc"];
                string insideUrl = khuVuc == "MB" ? insideUrlMB : insideUrlMN;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(insideUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("token", Token);
                    client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                    //client.Timeout = new TimeSpan(100);
                    response = await client.GetAsync(url);
                    log.Debug("Trạng thái của API " + client.BaseAddress + ": " + response.StatusCode);
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // Gọi lại khi token hết hạn
                        // Lưu lại token
                        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        configuration.AppSettings.Settings["tokenValue"].Value = "";
                        configuration.Save(ConfigurationSaveMode.Full, true);
                        ConfigurationManager.RefreshSection("appSettings");
                        await GetToken();
                        accessToken = ConfigurationManager.AppSettings["tokenValue"];
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("token", Token);
                        client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                        response = await (client.GetAsync(url));

                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotImplemented,
                    Content = new StringContent(JsonConvert.SerializeObject(new ApiResult { message = ex.Message }))
                };
            }
        }

        public static async Task<string> GetBase64Async(string accessToken, string url)
        {
            try
            {
                var khuVuc = ConfigurationManager.AppSettings["KhuVuc"];
                string insideUrl = khuVuc == "MB" ? insideUrlMB : insideUrlMN;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(insideUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("token", Token);
                    client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                    var bytes = await client.GetByteArrayAsync(url);
                    string base64 = Convert.ToBase64String(bytes);
                    return base64;
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return ex.Message;
            }
        }

        // CuongHM add
        public static async Task<string> PutJsonAsync(string accessToken, string url, string json)
        {
            try
            {
                var khuVuc = ConfigurationManager.AppSettings["KhuVuc"];
                string insideUrl = khuVuc == "MB" ? insideUrlMB : insideUrlMN;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(insideUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("token", Token);
                    client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(url, content);
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // Gọi lại khi token hết hạn
                        // Lưu lại token
                        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        configuration.AppSettings.Settings["tokenValue"].Value = "";
                        configuration.Save(ConfigurationSaveMode.Full, true);
                        ConfigurationManager.RefreshSection("appSettings");
                        await GetToken();
                        accessToken = ConfigurationManager.AppSettings["tokenValue"];
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("token", Token);
                        client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = await client.PutAsync(url, content);
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        return responseString;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return ex.Message;
            }
            return null;
        }

        public static async Task<HttpResponseMessage> PutJsonAsyncResponse(string accessToken, string url, string json)
        {
            try
            {
                var khuVuc = ConfigurationManager.AppSettings["KhuVuc"];
                string insideUrl = khuVuc == "MB" ? insideUrlMB : insideUrlMN;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(insideUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("token", Token);
                    client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(url, content);
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // Gọi lại khi token hết hạn
                        // Lưu lại token
                        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        configuration.AppSettings.Settings["tokenValue"].Value = "";
                        configuration.Save(ConfigurationSaveMode.Full, true);
                        ConfigurationManager.RefreshSection("appSettings");
                        await GetToken();
                        accessToken = ConfigurationManager.AppSettings["tokenValue"];
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("token", Token);
                        client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                        response = await client.PutAsync(url, content);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotImplemented,
                    Content = new StringContent(JsonConvert.SerializeObject(new ApiResult { message = ex.Message }))
                };
            }
            return null;
        }

        public static async Task<HttpResponseMessage> GetCheckConnectAPI(string maTrungTam)
        {
            HttpResponseMessage response = null;
            try
            {
                var khuVuc = ConfigurationManager.AppSettings["KhuVuc"];
                string insideUrl = khuVuc == "MB" ? insideUrlMB : insideUrlMN;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(insideUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("token", Token);
                    client.DefaultRequestHeaders.Add("siteCode", maTrungTam);
                    response = await Task.FromResult(client.GetAsync("eppws/services/rest/syncTran/logCheckConnection/" + maTrungTam).Result);
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.Error($"Failed: {ex.Message}\n {ex.StackTrace}");
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Forbidden,
                    Content = new StringContent(JsonConvert.SerializeObject(new ApiError { error = "invalid_token" }))
                };
            }
        }
    }
}
