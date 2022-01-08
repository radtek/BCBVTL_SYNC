using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sync.Core.Utils
{
    public class Utils
    {
        public static List<DropDownItem> lstTime = new List<DropDownItem>
        {
            new DropDownItem {VALUE=1,TEXT="Ngày" },
            new DropDownItem {VALUE=2,TEXT="Giờ" },
            new DropDownItem {VALUE=3,TEXT="Phút" },
            new DropDownItem {VALUE=4,TEXT="Giây" }
        };

        public static List<DropDownItem> lstUnit = new List<DropDownItem>
        {
            new DropDownItem {VALUE=311,TEXT="Hà Nội" },
            new DropDownItem {VALUE=2,TEXT="Hải Phòng" },
            new DropDownItem {VALUE=3,TEXT="Nghệ An" },
            new DropDownItem {VALUE=4,TEXT="Đà Nẵng" }
        };

        public static List<DropDownTextItem> lstLoaiHC = new List<DropDownTextItem>
        {
            new DropDownTextItem {VALUE="C",TEXT="Hộ chiếu đã in" },
            new DropDownTextItem {VALUE="R",TEXT="Hộ chiếu đã bị hủy" }
        };

        public static List<DropDownItem> lstSyncMethod = new List<DropDownItem>
        {
            new DropDownItem { VALUE=1,TEXT="Thủ công"},
            new DropDownItem { VALUE=2,TEXT="Tự động"}
        };

        public static string ConvertToUnAsign(string str)
        {
            string[] signs = new string[] {"aAeEoOuUiIdDyY","áàạảãâấầậẩẫăắằặẳẵ","ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ","éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ","óòọỏõôốồộổỗơớờợởỡ","ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ","úùụủũưứừựửữ","ÚÙỤỦŨƯỨỪỰỬỮ","íìịỉĩ","ÍÌỊỈĨ","đ","Đ","ýỳỵỷỹ","ÝỲỴỶỸ"
        };
            for (int i = 1; i < signs.Length; i++)
            {
                for (int j = 0; j < signs[i].Length; j++)
                {
                    str = str.Replace(signs[i][j], signs[0][i - 1]);
                }
            }
            return str;
        }

        public static long ConvertNowDateTimeToTimestamp()
        {
            Int64 retval = 0;
            var st = new DateTime(1970, 1, 1);
            TimeSpan t = (DateTime.Now.ToUniversalTime() - st);
            retval = (Int64)(t.TotalMilliseconds + 0.5);
            return retval * 10000 + 621355968000000000;
        }

        public static string CreateParam(List<OracleParameter> lstParam)
        {
            try
            {
                string result = "";
                var lstParamNew = lstParam.Where(x => x.ParameterName != "p_ERR_MESAGE").ToList();
                foreach (var param in lstParamNew)
                {
                    if (param.Direction == System.Data.ParameterDirection.Output)
                        continue;
                    string name = param.ParameterName;
                    var type = param.OracleDbType;
                    if (type == OracleDbType.Decimal)
                    {
                        try
                        {
                            decimal[] arr = (decimal[])param.Value;
                            for (int i = 0; i < arr.Length; i++)
                            {
                                result += name + "(" + (i + 1) + "):=" + arr[i] + ";" + Environment.NewLine;
                            }
                        }
                        catch
                        {
                            decimal?[] arr = (decimal?[])param.Value;
                            for (int i = 0; i < arr.Length; i++)
                            {
                                string value = arr[i] != null ? arr[i].ToString() : "NULL";
                                result += name + "(" + (i + 1) + "):=" + value + ";" + Environment.NewLine;
                            }
                        }
                    }
                    else if (type == OracleDbType.Varchar2)
                    {
                        var arr = ((string[])param.Value);
                        for (int i = 0; i < arr.Length; i++)
                        {
                            result += name + "(" + (i + 1) + "):='" + arr[i] + "'" + ";" + Environment.NewLine;
                        }
                    }
                    else if (type == OracleDbType.Date)
                    {
                        var arr = ((DateTime[])param.Value);
                        for (int i = 0; i < arr.Length; i++)
                        {
                            result += name + "(" + (i + 1) + "):='" + arr[i] + "'" + ";" + Environment.NewLine;
                            //result += name + "(" + (i + 1) + "):=" + "NULL" + "" + ";" + Environment.NewLine;
                        }
                    }
                    result += "-----------------------" + Environment.NewLine;
                }

                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static OracleDbType GetOracleDbType(string o)
        {
            if (o == "string") return OracleDbType.Varchar2;
            if (o == "datetime") return OracleDbType.Date;
            if (o == "int64") return OracleDbType.Int64;
            if (o == "int32") return OracleDbType.Int32;
            if (o == "int16") return OracleDbType.Int16;
            if (o == "sbyte") return OracleDbType.Byte;
            if (o == "byte") return OracleDbType.Int16;
            if (o == "decimal") return OracleDbType.Decimal;
            if (o == "float") return OracleDbType.Single;
            if (o == "double") return OracleDbType.Double;
            if (o == "byte[]") return OracleDbType.Blob;

            return OracleDbType.Varchar2;
        }

        /// <summary>
        /// Convert ngày tháng: 16/06/2017 -> 20170616
        /// </summary>
        public static string GetDate(string date)
        {
            string date8 = "";
            try
            {
                string[] datea = date.Split('/');
                date8 = datea[2] + datea[1] + datea[0];
            }
            catch { }
            return date8;
        }

        /// <summary>
        /// Convert ngày tháng: 2017-12-16 -> 20171215
        /// </summary>
        public static string GetDate8(string dateYYYY_MM_DD)
        {
            string date8 = "";
            try
            {
                date8 = dateYYYY_MM_DD.Replace("-", "");
            }
            catch { }
            return date8;
        }

        /// <summary>
        /// Convert ngày tháng: 16/12/2017 -> 2017-16-12
        /// </summary>
        public static string ConvertDate8(string dateDDMMYYYY)
        {
            try
            {
                string date8 = "";
                string[] arr = dateDDMMYYYY.Split('/');
                date8 = arr[2] + "-" + arr[1] + "-" + arr[0];
                return date8;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Convert ngày tháng: 20170616 -> 16/06/2017
        /// </summary>
        public static string LoadDate(string date8)
        {
            string date = "";
            try
            {
                date = date8.Substring(6, 2) + "/" + date8.Substring(4, 2) + "/" + date8.Substring(0, 4);
            }
            catch { }
            return date;
        }

        /// <summary>
        /// input: 20170620151200 --> 20/06/2017 15:12:00
        /// </summary>
        public static string LoadDateTime(string date14)
        {
            string date = "";
            try
            {
                date = date14.Substring(6, 2) + "/" + date14.Substring(4, 2) + "/" + date14.Substring(0, 4) + " " + date14.Substring(8, 2) + ":" + date14.Substring(10, 2) + ":" + date14.Substring(12, 2);
            }
            catch { }
            return date;
        }

        /// <summary>
        /// So sánh 2 ngày có dạng yyyyMMddHHmmss, return 0: bằng nhau, 1: nhỏ hơn, 2: lớn hơn
        /// </summary>
        public static int CompareDate14(string date14a, string date14b)
        {
            try
            {
                double date1 = double.Parse(date14a);
                double date2 = double.Parse(date14b);
                if (date1 == date2) return 0;
                if (date1 < date2) return 1;
                if (date1 > date2) return 2;
            }
            catch (Exception)
            { }
            return -1;
        }

        /// <summary>
        /// Convert ngày tháng: 20170616 -> 2017-06-06
        /// </summary>
        public static string LoadDateYYYY_MM_DD(string date8)
        {
            string date = "";
            try
            {
                date = date8.Substring(0, 4) + "-" + date8.Substring(4, 2) + "-" + date8.Substring(6, 2);
            }
            catch { }
            return date;
        }

        /// <summary>
        /// So sánh 2 ngày có dạng yyyyMMdd, return 0: bằng nhau, 1: nhỏ hơn, 2: lớn hơn
        /// </summary>
        public static int CompareDate8(string date8a, string date8b)
        {
            try
            {
                int date1 = Int32.Parse(date8a);
                int date2 = Int32.Parse(date8b);
                if (date1 == date2) return 0;
                if (date1 < date2) return 1;
                if (date1 > date2) return 2;
            }
            catch (Exception)
            { }
            return -1;
        }

        public static int SplitDate(string date)
        {
            try
            {
                return String.IsNullOrEmpty(date) ? -1 : Convert.ToInt32(date.Substring(0, 8));
            }
            catch (Exception) { }
            return -1;
        }

        public static string LoadPassportStatus(string status, string reason)
        {
            try
            {
                string dt = "";
                switch (status)
                {
                    case "INIT":
                        dt = "Khởi tạo";
                        break;
                    case "PACKED":
                        dt = "Đã đóng gói";
                        break;
                    case "PERSONALIZED":
                        dt = "Đã in";
                        break;
                    case "ISSUANCE":
                        dt = "Hiện hành";
                        break;
                    case "DAMAGED":
                        dt = "Hỏng";
                        break;
                    case "LOST":
                        dt = "Mất";
                        break;
                    case "CANCELLED":
                        switch (reason)
                        {
                            case "RENEW":
                                dt = "Đóng dấu hủy";
                                break;
                            default:
                                dt = "Hủy";
                                break;
                        }
                        break;
                    case "EXPIRED":
                        dt = "Hết hạn";
                        break;
                    case "NONE":
                        dt = "Không hiệu lực";
                        break;
                    default:
                        break;
                }
                return dt;
            }
            catch
            {
                return "";
            }
        }

        public static string ConvertDateSecond(string date)
        {
            try
            {
                if (!String.IsNullOrEmpty(date))
                {
                    string[] arr = date.Split(' ');
                    if (arr.Length > 1)
                    {
                        string[] arrH = arr[1].Split(':');
                        return GetDate(arr[0]) + arrH[0] + arrH[1] + arrH[2];
                    }
                    else
                    {
                        return GetDate(arr[0]);
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Chuyển string yyyymmdd sang DateTime 
        /// </summary>
        public static DateTime GetDateTime8(string date8)
        {
            try
            {
                int year = Int32.Parse(date8.Substring(0, 4));
                int month = Int32.Parse(date8.Substring(4, 2));
                int day = Int32.Parse(date8.Substring(6, 2));
                return new DateTime(year, month, day);
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
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

    }

    public class DropDownItem
    {
        public int VALUE { get; set; }
        public string TEXT { get; set; }
    }

    public class DropDownTextItem
    {
        public string TEXT { get; set; }
        public string VALUE { get; set; }
    }

    public class ApiResponseList
    {
        public string code { get; set; }
        public string message { get; set; }
        public List<string> data { get; set; }
    }

    public class TaskUrl
    {
        public const string GuiHoSoLenTTDH = "eppws/services/rest/syncTran/uploadTransaction";
        public const string GuiDSDeXuatLenA08 = "eppws/services/rest/syncTran/uploadHandoverA";
        public const string GuiTrangThaiDongBo = "eppws/services/rest/syncTran/updateStatusQueue";
        public const string GuiKetQuaTraHoChieuLenTTDH = "eppws/services/rest/syncTran/issuedPassport";
        public const string GuiTrangThaiNhanHoChieu = "eppws/services/rest/syncTran/updateStatus";
        public const string CapNhatHoSoLenTTDH = "eppws/services/rest/syncTran/updateTransaction";
        public const string GuiCongVanTraLoiA08 = "eppws/services/rest/syncXmns/uploadDocumentList/";
    }
}
