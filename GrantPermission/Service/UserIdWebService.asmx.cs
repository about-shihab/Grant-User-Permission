using GrantPermission.BLL;
using GrantPermission.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace GrantPermission.Service
{
    /// <summary>
    /// Summary description for UserIdWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserIdWebService : System.Web.Services.WebService
    {
        UserPermissionManager userPermissionManager = new UserPermissionManager();

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetAllUserId(string prefix)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ULTIMUS"].ConnectionString;
            List<string> userIdList = new List<string>();
            string sql = @"SELECT t.user_id FROM ULTIMUS.SMS_USER_PROFILE t
                           where user_id like '" + prefix+ @"%' and ROWNUM <= 10
                           order by t.user_id asc";
            using (OracleConnection connection =
                new OracleConnection())
            {
                connection.ConnectionString =
                    connectionString;

                try
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand(sql, connection);
                    command.CommandType = CommandType.Text;
                    OracleDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        userIdList.Add(dr["user_id"].ToString());
                    } 
                   
                    //dr.Read();
                    return userIdList.ToArray();

                }
                catch (OracleException ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        
    }
}
