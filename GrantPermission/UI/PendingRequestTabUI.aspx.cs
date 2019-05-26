using GrantPermission.BLL;
using GrantPermission.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrantPermission.UI
{
    public partial class PendingRequestTabUI : System.Web.UI.Page
    {
        private bool _refreshState;
        private bool _isRefresh;

        UserPermissionManager userPermissionManager = new UserPermissionManager();
        RoleManager roleManager = new RoleManager();
        //protected System.Web.UI.WebControls.Label RefreshID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_isRefresh)
            {
                Response.Redirect("PendingRequestTabUI.aspx");
            }


            if (Session["USER_ID"] != null)
            {


                if (int.Parse(Session["HO_USER_FLAG"].ToString()) == 0)
                {
                    Response.Redirect("PermissionRequestUI.aspx", true);


                }
                else
                {
                    //check head office user has admin role for permission
                    if (!userPermissionManager.CheckGrantAdminUser(Session["USER_ID"].ToString()))
                        Response.Redirect("PermissionRequestUI.aspx", true);
                }

                //if (RefreshID.Text.Length == 0)
                //{
                //    RefreshID.Text = Session.SessionID + DateTime.Now.Ticks.ToString();
                //}
                this.Master.userNameMaster = Session["USER_NAME"].ToString();
                this.Master.branchNameMaster = Session["BRANCH_NAME"].ToString();
            }
            if (Session["USER_ID"] == null)
            {
                Response.Redirect(LoginLink.UltimasLogin, true);

            }
            String hiddenFieldValue = hidLastTab.Value;
            StringBuilder js = new StringBuilder();
            js.Append("<script type='text/javascript'>");
            js.Append("var previouslySelectedTab = ");
            js.Append(hiddenFieldValue);
            js.Append(";</script>");

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "acttab", js.ToString());
            if (!IsPostBack)
            {

                this.BindAllRequest();

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

        public void BindPendingRequest()
        {
            int activeStatus = Convert.ToInt32(ConfigurationManager.AppSettings["PendingStatus"]);
            pendingRequestsGridView.DataSource = userPermissionManager.GetAllRequestByStatus(activeStatus);
            pendingRequestsGridView.DataBind();
        }
        public void BindRejectedRequest()
        {

            int activeStatus = Convert.ToInt32(ConfigurationManager.AppSettings["RejectStatus"]);
            rejectedRequestGridView.DataSource = userPermissionManager.GetAllRequestByStatus(activeStatus);
            rejectedRequestGridView.DataBind();

        }
        public void BindApprovedUser()
        {
            int activeStatus = Convert.ToInt32(ConfigurationManager.AppSettings["ApproveStatus"]);
            approvedUserGridView.DataSource = userPermissionManager.GetAllRequestByStatus(activeStatus);
            approvedUserGridView.DataBind();
        }

        public void BindAllRequest()
        {
            this.BindPendingRequest();
            this.BindRejectedRequest();
            this.BindApprovedUser();

        }
        protected void pendingRequestsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //check if the row is the header row
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //add the thead and tbody section programatically
                e.Row.TableSection = TableRowSection.TableHeader;
                e.Row.Font.Size = 9;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.TableSection = TableRowSection.TableBody;
                e.Row.Font.Size = 9;


            }
        }

        protected void pendingRequestsGridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //string sesToken = (string)Session["FrameworkConst.SYNC_CONTROL_KEYWORD"];
                //string pageToken = RefreshID.Text;
                //if (sesToken != null && sesToken != pageToken)
                //{
                //    Response.Write("The Refresh was performed after submit.");
                //}
                //else
                //{

                if (e.CommandName != "Approve" && e.CommandName != "Reject" && e.CommandName != "Approve All" && e.CommandName != "Reject All") return;

                if (e.CommandName == "Approve All")
                {
                    //approve all pending request
                    ApproveAllOrRejectAllHandler("p", 1);
                }
                else if (e.CommandName == "Reject All")
                {
                    //reject all pending request
                    ApproveAllOrRejectAllHandler("p", 2);
                }
                else
                {
                    //approve or reject single request
                    ApproveOrReject(e.CommandName, e.CommandArgument.ToString());
                }
                
                this.BindAllRequest();

                //Session["FrameworkConst.SYNC_CONTROL_KEYWORD"] =Session.SessionID + DateTime.Now.Ticks.ToString();
                //RefreshID.Text = sesToken; 
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        protected void rejectedRequestGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //check if the row is the header row
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //add the thead and tbody section programatically
                e.Row.TableSection = TableRowSection.TableHeader;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Font.Size = 9;

            }
        }

        protected void rejectedRequestGridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName != "Approve" && e.CommandName != "Approve All") return;
                if (e.CommandName == "Approve All")
                {
                    //approve all pending request
                    ApproveAllOrRejectAllHandler("r", 1);
                }
                else if (e.CommandName == "Approve")
                {
                    ApproveOrReject(e.CommandName, e.CommandArgument.ToString());
                }
                
                this.BindAllRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void approvedUserGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //check if the row is the header row
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //add the thead and tbody section programatically
                e.Row.TableSection = TableRowSection.TableHeader;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Font.Size = 9;

            }
        }


        public void ApproveAllOrRejectAllHandler(string handler, int activeStatus)
        {
            DataTable dtab = new DataTable();
            if (handler == "p")
            {
                //getting all pending requests
                dtab = userPermissionManager.GetAllRequestByStatus(Convert.ToInt32(ConfigurationManager.AppSettings["PendingStatus"]));

            }
            else if (handler == "r")
            {
                //getting all rejected requests
                dtab = userPermissionManager.GetAllRequestByStatus(Convert.ToInt32(ConfigurationManager.AppSettings["RejectStatus"]));
            }

            foreach (DataRow dr in dtab.Rows)
            {
                DataTable dt = roleManager.GetRoleByRoleName(dr["role_nm"].ToString());
                int roleId = Convert.ToInt32(dt.Rows[0][0]);
                int moduleId = Convert.ToInt32(dt.Rows[0][1]);
                string errorMessage = userPermissionManager.sp_GrantPermisson(dr["user_id"].ToString(), moduleId, roleId, Session["USER_ID"].ToString(), activeStatus);
                if (errorMessage == "")
                {

                    this.errorLabel.Text = " Performed for all ";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                    this.modalHeader.Style.Add("background-color", "green");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                    this.errorLabel.Text = errorMessage;
                    this.modalHeader.Style.Add("background-color", "orangered");
                }

            }
        }

        public void ApproveOrReject(string commandName, string commandArgument)
        {
            int activeStatus = 0;
            string[] arg = new string[2];
            arg = commandArgument.Split(';');
            string grantUserId = arg[0];
            string roleName = arg[1];
            string userId = Session["USER_ID"].ToString();
            if (commandName == "Approve")
            {
                activeStatus = Convert.ToInt32(ConfigurationManager.AppSettings["ApproveStatus"]);
            }
            else if (commandName == "Reject")
            {
                activeStatus = Convert.ToInt32(ConfigurationManager.AppSettings["RejectStatus"]);
            }

            DataTable dt = roleManager.GetRoleByRoleName(roleName);
            int roleId = Convert.ToInt32(dt.Rows[0][0]);
            int moduleId = Convert.ToInt32(dt.Rows[0][1]);
            string errorMessage = userPermissionManager.sp_GrantPermisson(grantUserId, moduleId, roleId, userId, activeStatus);
            if (errorMessage == "")
            {
                if (commandName == "Approve")
                    this.errorLabel.Text = " Permission Approved for " + grantUserId;

                else
                    this.errorLabel.Text = " Permission Rejected for " + grantUserId;

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                this.modalHeader.Style.Add("background-color", "green");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                this.errorLabel.Text = errorMessage;
                this.modalHeader.Style.Add("background-color", "orangered");
            }

        }
    }
}