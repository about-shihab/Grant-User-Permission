using GrantPermission.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrantPermission.UI
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_ID"] != null)
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();

                //Session.Remove("USER_ID");
                //Session.Remove("USER_NAME");
                //Session.Remove("BRANCH_NAME");
                //Session.Remove("BRANCH_CODE");
                //Session.Remove("HO_USER_FLAG");
                //Session["isLogin"] = "N";
            }
              Response.Redirect(LoginLink.UltimasLogin, true); 

            

        }
    }
}