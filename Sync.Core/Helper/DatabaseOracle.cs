using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace Sync.Core.Helper
{
    public class DatabaseOracle
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string connectionString { get; set; }
        public DatabaseOracle(string conStr)
        {
            connectionString = conStr;
        }

        #region Create connection

        public bool CheckConnect()
        {
            OracleConnection con = new OracleConnection(connectionString);
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                log.Error("Lỗi kết nối DB" + ex.Message);
                return false;
            }

        }
        #endregion

        #region Execute Table for query
        public DataTable ExecuteTable(string sql)
        {
            OracleConnection con = new OracleConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;           
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            //Connect();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                log.Error("ERROR: " + ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region Execute Non Query for query
        public async Task<int> ExecuteNonQuery(string sql)
        {
            OracleConnection con = new OracleConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OracleTransaction transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Transaction = transaction;
            cmd.CommandType = CommandType.Text;
            //Connect();
            try
            {
                var result = await cmd.ExecuteNonQueryAsync();
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                log.Error("ERROR: " + ex.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region Create Associative Array for Oracle Parameters
        public OracleParameter CreateDecimalAssociativeArray(string name, decimal[] value, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = new OracleParameter(name, OracleDbType.Decimal, direction);
            param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            param.Value = value;
            param.Size = value.Length;
            return param;
        }

        public OracleParameter CreateDecimalAssociativeArrayAllowNull(string name, decimal?[] value, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = new OracleParameter(name, OracleDbType.Decimal, direction);
            param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            param.Value = value;
            param.Size = value.Length;
            return param;
        }

        public OracleParameter CreateStringAssociativeArray(string name, string[] value, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = new OracleParameter(name, OracleDbType.Varchar2, direction);
            param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            param.Value = value;
            param.Size = value.Length;
            return param;
        }

        public OracleParameter CreateClobAssociativeArray(string name, string[] value, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = new OracleParameter(name, OracleDbType.Clob, direction);
            param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            param.Value = value;
            param.Size = value.Length;
            return param;
        }

        public OracleParameter CreateInt32AssociativeArray(string name, int[] value, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = new OracleParameter(name, OracleDbType.Int32, direction);
            param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            param.Value = value;
            param.Size = value.Length;
            return param;
        }

        public OracleParameter CreateDateAssociativeArray(string name, DateTime[] value, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = new OracleParameter(name, OracleDbType.Date, direction);
            param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            param.Value = value;
            param.Size = value.Length;
            return param;
        }

        public OracleParameter CreateDateAssociativeArrayAllowNull(string name, DateTime?[] value, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = new OracleParameter(name, OracleDbType.Date, direction);
            param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            param.Value = value;
            param.Size = value.Length;
            return param;
        }
        #endregion

        #region Execute Store Procedure return DataSet
        public async Task<DataSet> ExecuteProcDataSet(string procName, List<OracleParameter> lstParam)
        {
            var ds = new DataSet();
            OracleConnection con = new OracleConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //Connect();
            try
            {
                using (var cmd = new OracleCommand(procName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (lstParam.Count > 0)
                        foreach (var param in lstParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    await cmd.ExecuteNonQueryAsync();
                    var dtAdapter = new OracleDataAdapter(cmd);
                    dtAdapter.Fill(ds);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                log.Error(procName + " ERROR: " + ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region Execute Table for Store Procedure
        public async Task<DataTable> ExecuteProcTable(string procName, List<OracleParameter> lstParam)
        {
            var dt = new DataTable();
            OracleConnection con = new OracleConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //Connect();
            try
            {
                using (var cmd = new OracleCommand(procName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (lstParam.Count > 0)
                        foreach (var param in lstParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    var x = await cmd.ExecuteNonQueryAsync();
                    var dtAdapter = new OracleDataAdapter(cmd);
                    dtAdapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                log.Error(procName + " ERROR: " + ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region Parse Datatable To Dictionary
        public List<Dictionary<string, object>> ParseTableToDictionary(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return rows;
        }

        #endregion

        #region Execute Non Query for Store Procedure
        public async Task<int> ExecuteProcNonQuery(string procName, List<OracleParameter> lstParam)
        {
            //Connect();
            OracleConnection con = new OracleConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OracleTransaction transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                using (var cmd = new OracleCommand(procName, con))
                {
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (lstParam.Count > 0)
                        foreach (var param in lstParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    var result = await cmd.ExecuteNonQueryAsync();
                    transaction.Commit();
                    return result;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                log.Error(procName + " ERROR: " + ex.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }

        public async Task<int> ExecuteProcNonQueryClob(string procName, List<OracleParameter> lstParam)
        {
            //Connect();
            OracleConnection con = new OracleConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OracleTransaction transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                using (var cmd = new OracleCommand(procName, con))
                {
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (lstParam.Count > 0)
                        foreach (var param in lstParam)
                        {
                            if (param.OracleDbType == OracleDbType.Clob)
                            {
                                OracleClob clob = new OracleClob(con);
                                clob.Write(param.Value.ToString().ToArray(), 0, param.Value.ToString().Length);
                                param.Value = clob;
                            }
                            cmd.Parameters.Add(param);
                        }
                    var result = await cmd.ExecuteNonQueryAsync();
                    transaction.Commit();
                    return result;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                log.Error(procName + " ERROR: " + ex.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region Execute Non Query for Store Procedure with transaction
        public async Task<int> ExecuteProcNonQueryTS(string procName, List<OracleParameter> lstParam)
        {
            //Connect();
            OracleConnection con = new OracleConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OracleTransaction transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                using (var cmd = new OracleCommand(procName, con))
                {
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (lstParam.Count > 0)
                        foreach (var param in lstParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    var result = await cmd.ExecuteNonQueryAsync();
                    transaction.Commit();
                    return result;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                log.Error(procName + " ERROR: " + ex.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region Execute Store Procedure And Convert Result to List<T>

        public static List<string> InvalidJsonElements;

        public async Task<IList<T>> ExecuteProcToList<T>(string procName, List<OracleParameter> lstParam)
        {
            var dt = new DataTable();
            //serializer.MaxJsonLength = Int32.MaxValue;
            IList<T> objectsList = new List<T>();
            try
            {
                //Execute procedure and get table result
                dt = await ExecuteProcTable(procName, lstParam);

                if (dt.Rows.Count == 0) return objectsList;

                //Covert datatable to json string
                string jsonString = JsonConvert.SerializeObject(ParseTableToDictionary(dt));

                //Convert jsonString to List<T>
                InvalidJsonElements = null;
                var array = JArray.Parse(jsonString);

                foreach (var item in array)
                {
                    try
                    {
                        //Map single json in array to object<T>.
                        var itemMapped = item.ToObject<T>();

                        objectsList.Add(itemMapped);
                    }
                    catch (Exception)
                    {
                        InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                        InvalidJsonElements.Add(item.ToString());
                    }
                }
                return objectsList;
            }
            catch (Exception ex)
            {
                log.Error(procName + " ERROR: " + ex.Message);
                return null;
            }
        }

        #endregion

        #region Convert DataTable to Json
        public string ConvertDataTabletoJson(DataTable dt)
        {
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return JsonConvert.SerializeObject(rows);
        }
        #endregion

        #region Convert Table To List<T>

        public List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            //serializer.MaxJsonLength = Int32.MaxValue;
            var objectsList = new List<T>();
            try
            {
                if (dt.Rows.Count == 0) return objectsList;

                //Covert datatable to json string
                var rows = dt.AsEnumerable()
                       .Select(r => r.Table.Columns.Cast<DataColumn>()
                               .Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])
                              ).ToDictionary(z => z.Key, z => z.Value)
                       ).ToList();

                string jsonTable = JsonConvert.SerializeObject(rows);

                //Convert jsonString to List<T>
                InvalidJsonElements = null;
                var array = JArray.Parse(jsonTable);

                foreach (var item in array)
                {
                    try
                    {
                        //Map single json in array to object<T>.
                        var itemMapped = item.ToObject<T>();
                        objectsList.Add(itemMapped);
                    }
                    catch (Exception ex)
                    {
                        InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                        InvalidJsonElements.Add(item.ToString());
                        log.Error(ex.Message);
                    }
                }
                return objectsList;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }
        #endregion

        #region Execute proc table and convert to Json
        public async Task<string> ExecuteProcToJson(string procName, List<OracleParameter> lstParam)
        {
            try
            {
                DataTable dt = await ExecuteProcTable(procName, lstParam);
                var json = ConvertDataTabletoJson(dt);
                return json;
            }
            catch (Exception ex)
            {
                log.Error(procName + " ERROR: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region Create Params for Stored Procedure
        public List<OracleParameter> CreateParams<T>(T item) where T : new()
        {
            try
            {
                List<OracleParameter> lstParams = new List<OracleParameter>();
                foreach (PropertyInfo property in item.GetType().GetProperties())
                {
                    var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                    var paramType = property.PropertyType.Name.ToLower().Contains("null") ? underlyingType.Name.ToLower() : property.PropertyType.Name.ToLower();
                    if (!property.ToString().ToLower().Contains("collection"))
                        lstParams.Add(new OracleParameter("p_" + property.Name.ToString(), Utils.Utils.GetOracleDbType(paramType), property.GetValue(item, null), ParameterDirection.Input));
                }
                return lstParams;
            }
            catch (Exception ex)
            {
                log.Error("ERROR: " + ex.Message);
                return null;
            }

        }
        #endregion

        public T GetXmlSync<T>(List<OracleParameter> lstParam, string procName) where T : ExcuteQueryResult, new()
        {
            T result = new T();
            OracleConnection con = new OracleConnection(connectionString);
            try
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    result.ConnectDB = true;
                }
                catch (Exception)
                {
                    result.ConnectDB = false;
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
                    var resultProc = cmd.ExecuteNonQuery();
                    var p_MSG = "OK";
                    if (lstParam.Any(x => x.ParameterName.ToLower() == "pmsg"))
                        p_MSG = lstParam.FirstOrDefault(x => x.ParameterName.ToLower() == "pmsg").Value.ToString();
                    if (resultProc == -1 && p_MSG == "OK")
                    {
                        result.Success = true;
                        if (lstParam.Any(x => x.ParameterName.ToLower() == "pxml"))
                        {
                            var p_OUT_XML = (OracleClob)lstParam.FirstOrDefault(x => x.ParameterName == "pXml").Value;
                            result.XML_DATA = (p_OUT_XML != null && p_OUT_XML.Value != null) ? p_OUT_XML.Value : "";
                        }
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = p_MSG;
                    }
                    if (con.State == ConnectionState.Open)
                        con.Close();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                result.Success = false;
                result.Message = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return result;
        }
    }
}
