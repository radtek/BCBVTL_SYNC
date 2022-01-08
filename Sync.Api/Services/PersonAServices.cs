using Oracle.ManagedDataAccess.Client;
using Sync.Api.Models;
using Sync.Core.Helper;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Sync.Api.Services
{
    public class PersonAServices : BaseService
    {

        /// <summary>
        /// Create buf_xml
        /// <param name="personId"></param>
        /// <returns></returns>
        public async Task<ResultCallProc> CreatePersonXml(string xmlData, string transactionId)
        {
            var result = new ResultCallProc();
            
            //log.Info("================= Lấy hồ sơ =================");
            //log.Info(xmlData);
            //log.Info("================= Kết thúc lấy hồ sơ =================");
            List<OracleParameter> lstParam = new List<OracleParameter> { };
            lstParam.Add(new OracleParameter("pXmlData", OracleDbType.Clob, xmlData, ParameterDirection.Input));
            lstParam.Add(new OracleParameter("pCode", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output));
            lstParam.Add(new OracleParameter("pMsg", OracleDbType.Varchar2, 1000, "", ParameterDirection.Output));

            var resultProc = await databaseOracle.ExecuteProcNonQuery("PKG_SYNC_TTDH_RETURN_BUFF.insert_new_document_v5", lstParam);
            var p_ERR_CODE = lstParam[lstParam.Count - 1].Value.ToString();
            if (resultProc == -1 && p_ERR_CODE == "OK")
            {
                result.success = true;
                result.message = "SUCC - " + transactionId;
            }
            else
            {
                result.success = false;
                result.message = "FAIL - " + transactionId + " - " + p_ERR_CODE;
            }

            return result;
        }
    }
}