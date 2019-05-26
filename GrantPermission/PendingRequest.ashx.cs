using GrantPermission.BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace GrantPermission.UI
{
    /// <summary>
    /// Summary description for PendingRequest
    /// </summary>
    public class PendingRequest : IHttpHandler
    {
        UserPermissionManager userPermissionManager = new UserPermissionManager();        

        public void ProcessRequest(HttpContext context)
        {
            System.Collections.Specialized.NameValueCollection forms = context.Request.Form;
            string strOperation = forms.Get("oper");

            string strResponse = string.Empty;

            DataTable dt = new DataTable();
            dt = userPermissionManager.GetAllPendingUser();
            //List<DataRow> pendingUserLists = dt.AsEnumerable().ToList();

            if (strOperation == null)
            {
                //oper = null which means its first load.
                var jsonSerializer = new JavaScriptSerializer();
                context.Response.Write(JsonConvert.SerializeObject(dt));
                //context.Response.Write(jsonSerializer.Serialize(dt.AsEnumerable().ToList()));
            }
            else if (strOperation == "del")
            {
               /* var query = Query.EQ("_id", forms.Get("EmpId").ToString());
               / collectionEmployee.Remove(query);
                strResponse = "Employee record successfully removed";
                context.Response.Write(strResponse);*/
            }
            else
            {
                /* string strOut=string.Empty;
                 AddEdit(forms, collectionEmployee, out strOut);
                 context.Response.Write(strOut);*/
            }
         }

        public void UpdateRequest()
        {

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}