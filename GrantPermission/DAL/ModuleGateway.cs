using System;
//using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
 

namespace GrantPermission.DAL
{
    public class ModuleGateway
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ULTIMUS"].ConnectionString;
        //public DataTable GetAllModule()
        //{
        //    DataTable dtab = new DataTable();
        //    string sql = "SELECT * FROM SEBL_MIS_SYS_MODULE_PARAM";
        //    using (OracleConnection connection =
        //        new OracleConnection())
        //    {
        //        connection.ConnectionString =
        //            connectionString;

        //        try
        //        {
        //            connection.Open();
        //            OracleCommand command = new OracleCommand(sql, connection);
        //            command.CommandType = CommandType.Text;
        //            OracleDataReader dr = command.ExecuteReader();

        //            //dr.Read();
        //            dtab.Load(dr);
        //            return dtab;

        //        } 
        //        catch (OracleException ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}

        public DataTable GetAllModule()
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
                    command.CommandText = "pkg_mis_system.fsp_get_module";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("pmodule_id", OracleType.Number, 3).Value = DBNull.Value;
                    command.Parameters.Add("pmodule_code", OracleType.VarChar, 10).Value = DBNull.Value;
                    command.Parameters.Add("perrorcode", OracleType.Int32, 5).Direction = ParameterDirection.Output;
                    command.Parameters.Add("perrormsg", OracleType.VarChar, 2000).Direction = ParameterDirection.Output;
                    command.Parameters.Add("presult_set_cur", OracleType.Cursor).Direction = ParameterDirection.Output;
                    OracleDataReader dr = command.ExecuteReader();
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
    }
}