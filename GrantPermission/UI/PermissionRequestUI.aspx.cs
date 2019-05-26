using GrantPermission.BLL;
using GrantPermission.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrantPermission.UI
{
    public partial class PermissionRequestUI : System.Web.UI.Page
    {
        ModuleManager moduleManager = new ModuleManager();
        RoleManager roleManager = new RoleManager();
        UserPermissionManager userPermissionManager = new UserPermissionManager();

        private bool _refreshState;
        private bool _isRefresh;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_isRefresh)
            {
                Response.Redirect("PermissionRequestUI.aspx");
            }

            if (Session["USER_ID"] != null)
            {
                
                userIdLabel.Text = Session["USER_ID"].ToString();
                this.Master.userNameMaster = Session["USER_NAME"].ToString();
                this.Master.branchNameMaster = Session["BRANCH_NAME"].ToString();
            }
            if (Session["USER_ID"] == null)
            {
                Response.Redirect(LoginLink.UltimasLogin, true); 

            }
            if (!IsPostBack)
            {

                moduleDropDownList.DataSource = moduleManager.GetAllModule();

                moduleDropDownList.DataTextField = "MODULE_NM";
                moduleDropDownList.DataValueField = "MODULE_ID";
                moduleDropDownList.DataBind();
                //moduleDropDownList.Items.Insert(0, new ListItem("--Select Module--", "0"));

            }

        }

        protected override void LoadViewState(object savedState)
        {
            object[] AllStates = (object[])savedState;
            base.LoadViewState(AllStates[0]);
            _refreshState = bool.Parse(AllStates[1].ToString());
            _isRefresh = _refreshState == bool.Parse(Session["__ISREFRESH"].ToString());
        }

        protected override object SaveViewState()
        {
            Session["__ISREFRESH"] = _refreshState;
            object[] AllStates = new object[2];
            AllStates[0] = base.SaveViewState();
            AllStates[1] = !(_refreshState);
            return AllStates;
        }
        protected void moduleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            roleDropDownList.Items.Clear();
            string selectModule = moduleDropDownList.SelectedItem.Value.ToString();
            if (selectModule == "")
            {
                roleDropDownList.Items.Clear();
            }
            else
            {
                roleDropDownList.DataSource = roleManager.GetAllRoleByModule(Session["USER_ID"].ToString(), Convert.ToInt32(moduleDropDownList.SelectedItem.Value));
                roleDropDownList.DataTextField = "ROLE_NM";
                roleDropDownList.DataValueField = "ROLE_ID";
                roleDropDownList.DataBind();
            }
        }

        protected void submitRequest_Click(object sender, EventArgs e)
        {
            string grantUserId = Session["USER_ID"].ToString(), userId = "Admin";
            int moduleId = Convert.ToInt32(moduleDropDownList.SelectedItem.Value);
            int? roleId = string.IsNullOrEmpty(roleDropDownList.SelectedItem.Value) ? (int?)null : int.Parse(roleDropDownList.SelectedItem.Value);
            int activeStatus = Convert.ToInt32(ConfigurationManager.AppSettings["PendingStatus"]);
            string errorMessage = userPermissionManager.sp_GrantPermisson(grantUserId, moduleId, roleId, userId, activeStatus);
            if (errorMessage == "")
            {

                errorLabel.Text = moduleDropDownList.SelectedItem + " Permission Request Submitted for " + grantUserId;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                this.modalHeader.Style.Add("background-color", "green");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                errorLabel.Text = errorMessage;
                this.modalHeader.Style.Add("background-color", "orangered");
            }
            

        }




        //protected void Submit(object sender, EventArgs e)
        //{
        //    string customerName = Request.Form[userIdTextBox.UniqueID];
        //    string customerId = Request.Form[hfUserId.UniqueID];
        //}
        //demo

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}