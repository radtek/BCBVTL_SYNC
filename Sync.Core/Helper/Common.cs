using log4net;
using Newtonsoft.Json;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace Sync.Core.Helper
{
    public class Common
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ApiBase apiBase = null;
        public Common()
        {
            apiBase = new ApiBase();
        }

        public static T DeserializeXMLFileToObject<T>(string XmlData)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlData)) return default(T);

            try
            {
                string strXmlData = XmlData.Replace("\n", "").Replace(" ", "");
                using (var stringReader = new StringReader(XmlData))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    returnObject = (T)serializer.Deserialize(stringReader);
                }
                //XmlSerializer serializer = new XmlSerializer(typeof(T));
                //MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(XmlData));
                //T resultingMessage = (T)serializer.Deserialize(memStream);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return returnObject;
        }


        /// <summary>
        /// Chuyển đổi từ model sang xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ConvertObjectToXML<T>(T model)
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter, model);
                return stringwriter.ToString();
            }
        }


        //public string GetToken()
        //{
        //    var accessToken = "";
        //    try
        //    {

        //        if (accessToken == "")
        //        {
        //            var resultAPI = (apiBase.GetToken()).Result;
        //            JsonAPI getToken = JsonConvert.DeserializeObject<JsonAPI>(resultAPI.data);
        //            if (getToken.Status == "00")
        //                log.Info("Không lấy được thông tin token !");
        //            else
        //                accessToken = getToken.Data;
        //        }
        //        return accessToken;
        //    }
        //    catch (Exception)
        //    {
        //        log.Error("Không kết nối được tới API...");
        //        return "";
        //    }
        //}
        public void SetTokenValue(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string XmlToJSON(string xml, List<string> listNodeList)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            return XmlToJSON(doc, listNodeList);
        }
        public static string XmlToJSON(XmlDocument xmlDoc, List<string> listNodeList)
        {
            StringBuilder sbJSON = new StringBuilder();
            sbJSON.Append("{ ");
            XmlToJSONnode(sbJSON, xmlDoc.DocumentElement, true, listNodeList);
            sbJSON.Append("}");
            return sbJSON.ToString();
        }

        //  XmlToJSONnode:  Output an XmlElement, possibly as part of a higher array
        private static void XmlToJSONnode(StringBuilder sbJSON, XmlElement node, bool showNodeName, List<string> listNodeList)
        {
            if (showNodeName)
                sbJSON.Append("\"" + SafeJSON(node.Name) + "\": ");
            sbJSON.Append("{");
            // Build a sorted list of key-value pairs
            //  where   key is case-sensitive nodeName
            //          value is an ArrayList of string or XmlElement
            //  so that we know whether the nodeName is an array or not.
            SortedList<string, object> childNodeNames = new SortedList<string, object>();

            //  Add in all node attributes
            if (node.Attributes != null)
                foreach (XmlAttribute attr in node.Attributes)
                    StoreChildNode(childNodeNames, attr.Name, attr.InnerText);

            //  Add in all nodes
            foreach (XmlNode cnode in node.ChildNodes)
            {
                if (cnode is XmlText)
                    StoreChildNode(childNodeNames, "value", cnode.InnerText);
                else if (cnode is XmlElement)
                    StoreChildNode(childNodeNames, cnode.Name, cnode);
            }

            // Now output all stored info
            foreach (string childname in childNodeNames.Keys)
            {
                bool check = listNodeList.Contains(childname);
                List<object> alChild = (List<object>)childNodeNames[childname];
                if (check)
                {
                    sbJSON.Append(" \"" + SafeJSON(childname) + "\": [ ");
                    foreach (object Child in alChild)
                        OutputNode(childname, Child, sbJSON, false, listNodeList);
                    sbJSON.Remove(sbJSON.Length - 2, 2);
                    sbJSON.Append(" ], ");
                }
                else
                {
                    if (alChild.Count == 1)
                        OutputNode(childname, alChild[0], sbJSON, true, listNodeList);
                    else
                    {
                        sbJSON.Append(" \"" + SafeJSON(childname) + "\": [ ");
                        foreach (object Child in alChild)
                            OutputNode(childname, Child, sbJSON, false, listNodeList);
                        sbJSON.Remove(sbJSON.Length - 2, 2);
                        sbJSON.Append(" ], ");
                    }
                }
            }
            sbJSON.Remove(sbJSON.Length - 2, 2);
            sbJSON.Append(" }");
        }

        //  StoreChildNode: Store data associated with each nodeName
        //                  so that we know whether the nodeName is an array or not.
        private static void StoreChildNode(SortedList<string, object> childNodeNames, string nodeName, object nodeValue)
        {
            // Pre-process contraction of XmlElement-s
            if (nodeValue is XmlElement)
            {
                // Convert  <aa></aa> into "aa":null
                //          <aa>xx</aa> into "aa":"xx"
                XmlNode cnode = (XmlNode)nodeValue;
                if (cnode.Attributes.Count == 0)
                {
                    XmlNodeList children = cnode.ChildNodes;
                    if (children.Count == 0)
                        nodeValue = null;
                    else if (children.Count == 1 && (children[0] is XmlText))
                        nodeValue = ((XmlText)(children[0])).InnerText;
                }
            }
            // Add nodeValue to ArrayList associated with each nodeName
            // If nodeName doesn't exist then add it
            List<object> ValuesAL;

            if (childNodeNames.ContainsKey(nodeName))
            {
                ValuesAL = (List<object>)childNodeNames[nodeName];
            }
            else
            {
                ValuesAL = new List<object>();
                childNodeNames[nodeName] = ValuesAL;
            }
            ValuesAL.Add(nodeValue);
        }

        private static void OutputNode(string childname, object alChild, StringBuilder sbJSON, bool showNodeName, List<string> listNodeList)
        {
            if (alChild == null)
            {
                if (showNodeName)
                    sbJSON.Append("\"" + SafeJSON(childname) + "\": ");
                sbJSON.Append("null");
            }
            else if (alChild is string)
            {
                if (showNodeName)
                    sbJSON.Append("\"" + SafeJSON(childname) + "\": ");
                string sChild = (string)alChild;
                sChild = sChild.Trim();
                sbJSON.Append("\"" + SafeJSON(sChild) + "\"");
            }
            else
                XmlToJSONnode(sbJSON, (XmlElement)alChild, showNodeName, listNodeList);
            sbJSON.Append(", ");
        }

        // Make a string safe for JSON
        private static string SafeJSON(string sIn)
        {
            StringBuilder sbOut = new StringBuilder(sIn.Length);
            foreach (char ch in sIn)
            {
                if (Char.IsControl(ch) || ch == '\'')
                {
                    int ich = (int)ch;
                    sbOut.Append(@"\u" + ich.ToString("x4"));
                    continue;
                }
                else if (ch == '\"' || ch == '\\' || ch == '/')
                {
                    sbOut.Append('\\');
                }
                sbOut.Append(ch);
            }
            return sbOut.ToString();
        }


        //public static void UpdateSoureCode()
        //{

        //    var versionServer = GetVersionServer();
        //    var currentVersion = ConfigurationManager.AppSettings["CurrentVersion"].ToString();
        //    if (string.IsNullOrEmpty(versionServer))
        //        versionServer = currentVersion;
        //    if (versionServer != currentVersion)
        //    {
        //        var pathApp = AppDomain.CurrentDomain.BaseDirectory;
        //        if (!string.IsNullOrEmpty(pathApp))
        //        {
        //            // Cập nhật lại version
        //            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //            configuration.AppSettings.Settings["CurrentVersion"].Value = versionServer;
        //            configuration.Save(ConfigurationSaveMode.Full, true);
        //            ConfigurationManager.RefreshSection("appSettings");


        //            //Run app update
        //            var pathProjectUpdate = ConfigurationManager.AppSettings["Project_Sync_Update"].ToString();
        //            Process.Start(pathProjectUpdate);
        //            // close app
        //            // WinForms app
        //            if (System.Windows.Forms.Application.MessageLoop)
        //            {
        //                // Use this since we are a WinForms app
        //                System.Windows.Forms.Application.Exit();
        //            }
        //            else
        //            {
        //                // Use this since we are a console app
        //                System.Environment.Exit(1);
        //            }

        //            // Coppy code in server
        //            //DownloadFile(pathApp);

        //        }
        //    }
        //}

        //public static string GetVersionServer()
        //{
        //    var result = "";
        //    try
        //    {
        //        string Server_UserName = ConfigurationManager.AppSettings["Server_UserName"].ToString();
        //        string Server_Password = ConfigurationManager.AppSettings["Server_Password"].ToString();
        //        string pathFile = ConfigurationManager.AppSettings["Server_PathFileVersion"].ToString();
        //        var fileVersionName = Path.GetFileName(pathFile);
        //        string pathFolder = pathFile.Replace(@"\" + fileVersionName, "");
        //        using (new NetworkConnection(pathFolder, new System.Net.NetworkCredential(Server_UserName, Server_Password)))
        //        {
        //            string[] lines = System.IO.File.ReadAllLines(pathFile);
        //            if (lines != null && lines.Length > 0)
        //            {
        //                result = lines[0];
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        log.Error("Lỗi khi lấy version trên server: " + ex.Message);
        //    }

        //    return result;
        //}

        /// <summary>
        /// Chuyển sang tiếng việt không dấu
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string KhongDau(string str)
        {
            str = str.Replace("à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ", "a");
            str = str.Replace("è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ", "e");
            str = str.Replace("ì|í|ị|ỉ|ĩ", "i");
            str = str.Replace("ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ", "o");
            str = str.Replace("ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ", "u");
            str = str.Replace("ỳ|ý|ỵ|ỷ|ỹ", "y");
            str = str.Replace("đ", "d");

            str = str.Replace("À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ", "A");
            str = str.Replace("È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ", "E");
            str = str.Replace("Ì|Í|Ị|Ỉ|Ĩ", "I");
            str = str.Replace("Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ", "O");
            str = str.Replace("Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ", "U");
            str = str.Replace("Ỳ|Ý|Ỵ|Ỷ|Ỹ", "Y");
            str = str.Replace("Đ", "D");
            return str;
        }

        /// <summary>
        /// Chuyển đổi từ object sang json
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ConvertObjectToJson(object model)
        {
            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };

            var json = serializer.Serialize(model);
            return json;
        }
    }
}
