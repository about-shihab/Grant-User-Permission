using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace GrantPermission.DAL
{
    public class RoleGateway
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ULTIMUS"].ConnectionString;
        public DataTable GetAllRoleByModule(int moduleId,string branchFlag)
        {
            DataTable dtab = new DataTable();
            string sql = @"SELECT t.* FROM SEBL_MIS_USER_ROLE_PARAM t
                            WHERE t.MODULE_ID="+moduleId+@"
                              AND upper(t.role_code) like '%"+branchFlag+"'";
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

                    //dr.Read();
                    dtab.Load(dr);
                    return dtab;

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

        public DataTable GetRoleByRoleName(string roleName)
        {
            DataTable dtab = new DataTable();
            string sql = @"SELECT t.role_id,t.module_id
                             FROM SEBL_MIS_USER_ROLE_PARAM t 
                            WHERE t.role_nm='"+ roleName+"'";
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

                    //dr.Read();
                    dtab.Load(dr);
                    return dtab;

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