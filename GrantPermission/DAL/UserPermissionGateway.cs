using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace GrantPermission.DAL
{
    public class UserPermissionGateway
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ULTIMUS"].ConnectionString;

        public string sp_GrantPermisson(string pgrant_user_id, int pmodule_id, int? prole_id, string puser_id, int active_status)
        {
            
            OracleDataAdapter adp = new OracleDataAdapter();
            using (OracleConnection connection = new OracleConnection())
            {

                connection.ConnectionString = connectionString;
                try
                {

                    connection.Open();
                    OracleCommand command = new OracleCommand();
                    command.Connection = connection;
                    command.CommandText = "fsp_add_grant_user_permission";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("pgrant_user_id", OracleType.NVarChar).Value = pgrant_user_id;
                    command.Parameters.Add("pmodule_id", OracleType.Number).Value = pmodule_id;
                    command.Parameters.Add("prole_id", OracleType.Number).Value = prole_id;
                    command.Parameters.Add("puser_id", OracleType.NVarChar).Value = puser_id;
                    command.Parameters.Add("pactive_status", OracleType.Number).Value = active_status;
                    command.Parameters.Add("perrorcode", OracleType.Int32, 5).Direction = ParameterDirection.Output;
                    command.Parameters.Add("perrormsg", OracleType.VarChar, 2000).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();
                    string perrormsg = command.Parameters["perrormsg"].Value.ToString();
                    command.Parameters.Clear();
                    return perrormsg;
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

        public DataTable GetAllGrantUser()
        {
            DataTable dtab = new DataTable();
            string sql = @"SELECT X.USER_ID,  Y.ROLE_CODE, Y.MODULE_NM,X.MAKE_BY,X.MAKE_DT
                                              FROM (SELECT *
                                                      FROM SEBL_USER_GRANT_MENU A
                                                     INNER JOIN SEBL_MIS_USER_ROLE_MENU B
                                                        ON A.MENU_ID = B.MENU_ID) X
                                              JOIN (SELECT *
                                                      FROM SEBL_MIS_USER_ROLE_PARAM T
                                                      JOIN SEBL_MIS_SYS_MODULE_PARAM U
                                                        ON T.MODULE_ID = U.MODULE_ID) Y
                                                ON X.ROLE_ID = Y.ROLE_ID
                                                WHERE ROWNUM<=100
                                             ORDER BY X.MAKE_DT DESC";//WHERE ROWNUM<=10
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


        public DataTable GetUserBasicInfo(string puser_id)
        {
            DataTable dtab = new DataTable();
            OracleDataAdapter adp = new OracleDataAdapter();
            using (OracleConnection connection = new OracleConnection())
            {

                connection.ConnectionString = connectionString;
                try
                {

                    connection.Open();
                    OracleCommand command = new OracleCommand();
                    command.Connection = connection;
                    command.CommandText = "pkg_mis_system.fsp_get_usr_basic_info";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("puser_login_name", OracleType.NVarChar).Value = puser_id;
                    command.Parameters.Add("pmodule_id", OracleType.Number).Value = DBNull.Value;
                    command.Parameters.Add("perrorcode", OracleType.Int32, 5).Direction = ParameterDirection.Output;
                    command.Parameters.Add("perrormsg", OracleType.VarChar, 2000).Direction = ParameterDirection.Output;
                    command.Parameters.Add("presult_set_cur", OracleType.Cursor).Direction = ParameterDirection.Output;

                    OracleDataReader dr = command.ExecuteReader();
                    string perrormsg = command.Parameters["perrormsg"].Value.ToString();
                    dtab.Load(dr);
                    command.Parameters.Clear();
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
        public DataTable GetUserBasicInfo(string puser_id,int moduleId)
        {
            DataTable dtab = new DataTable();
            OracleDataAdapter adp = new OracleDataAdapter();
            using (OracleConnection connection = new OracleConnection())
            {

                connection.ConnectionString = connectionString;
                try
                {

                    connection.Open();
                    OracleCommand command = new OracleCommand();
                    command.Connection = connection;
                    command.CommandText = "pkg_mis_system.fsp_get_usr_basic_info";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("puser_login_name", OracleType.NVarChar).Value = puser_id;
                    command.Parameters.Add("pmodule_id", OracleType.Number).Value = moduleId;
                    command.Parameters.Add("perrorcode", OracleType.Int32, 5).Direction = ParameterDirection.Output;
                    command.Parameters.Add("perrormsg", OracleType.VarChar, 2000).Direction = ParameterDirection.Output;
                    command.Parameters.Add("presult_set_cur", OracleType.Cursor).Direction = ParameterDirection.Output;

                    OracleDataReader dr = command.ExecuteReader();
                    string perrormsg = command.Parameters["perrormsg"].Value.ToString();
                    dtab.Load(dr);
                    command.Parameters.Clear();
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



       public DataTable GetAllPendingUser()
        {
           
            DataTable dtab = new DataTable();
            DataSet ds = new DataSet();
            string sql = @"SELECT t.user_id,r.role_nm  ,m.module_nm,t.request_date
                            FROM SEBL_USER_GRANT_ROLE t
                            JOIN sebl_mis_user_role_param r
                              ON t.role_id=r.role_id
                            JOIN sebl_mis_sys_module_param m 
                              ON m.module_id=r.module_id
                           WHERE T.active_status=0";

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
                    //OracleDataReader dr = command.ExecuteReader();
                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    //dr.Read();
                    //dtab.Load(dr);
                    da.Fill(dtab);
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


       public DataTable GetAllRequestByStatus(int activeStatus)
       {

           DataTable dtab = new DataTable();
           DataSet ds = new DataSet();
           string sql = @"SELECT T.USER_ID
                                 ,b.BRANCH_NM
                                 ,R.ROLE_NM
                                 ,M.MODULE_NM
                                 ,T.REQUEST_DATE
                                 ,T.AUTH_BY
                            FROM SEBL_USER_GRANT_ROLE T
                            JOIN sms_user_profile u
                              ON t.user_id=u.user_id
                            JOIN BRANCH_HOME_BANK b
                              ON u.home_branch_id=b.branch_id
                            JOIN SEBL_MIS_USER_ROLE_PARAM R
                              ON T.ROLE_ID = R.ROLE_ID
                            JOIN SEBL_MIS_SYS_MODULE_PARAM M
                              ON M.MODULE_ID = R.MODULE_ID
                           WHERE T.ACTIVE_STATUS=" + activeStatus;

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
                   //OracleDataReader dr = command.ExecuteReader();
                   OracleDataAdapter da = new OracleDataAdapter();
                   da.SelectCommand = command;
                   //dr.Read();
                   //dtab.Load(dr);
                   da.Fill(dtab);
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

       public bool CheckGrantAdminUser(string userId)
       {
           DataTable dtab = new DataTable();
           DataSet ds = new DataSet();
           string sql = @"SELECT COUNT(1) AS chk_user
                            FROM SEBL_USER_GRANT_ADMIN_USER t
                           WHERE t.user_id='"+ userId+"'";

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
                   if (dr.Read())
                   {
                       int count = int.Parse(dr["chk_user"].ToString());
                       if (count == 1)
                       {
                           return true;
                       }
                   }
                   
                   return false;

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