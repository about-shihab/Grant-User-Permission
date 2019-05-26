using GrantPermission.BLL;
using GrantPermission.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrantPermission.UI
{
    public partial class LoggedUserUI : System.Web.UI.Page
    {
        UserPermissionManager userPermissionManager = new UserPermissionManager();        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_ID"] == null)
            {
                Response.Redirect(LoginLink.UltimasLogin, true);

            }

            DataTable dtab = userPermissionManager.GetUserBasicInfo(Session["USER_ID"].ToString());
            DataRow row = dtab.Rows[0];
            Session["USER_NAME"] = row["USER_NM"].ToString();
            Session["BRANCH_NAME"] = row["BRANCH_NM"].ToString();
            Session["BRANCH_CODE"] = row["BRANCH_ID"].ToString();

            this.Master.userNameMaster = Session["USER_NAME"].ToString();
            this.Master.branchNameMaster = Session["BRANCH_NAME"].ToString();

            Session["HO_USER_FLAG"] = Convert.ToInt32(row["HO_USER_FLAG"]);
            if (int.Parse(Session["HO_USER_FLAG"].ToString()) == 1)
            {
                //check head office user has admin role for permission
                if (userPermissionManager.CheckGrantAdminUser(Session["USER_ID"].ToString()))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);

                }
                else
                {
                    Response.Redirect("PermissionRequestUI.aspx", true);
                }
            }
            else
            {
                Response.Redirect("PermissionRequestUI.aspx", true);

            }

        }
    }
}