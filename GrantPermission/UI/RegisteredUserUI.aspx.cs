using GrantPermission.BLL;
using GrantPermission.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrantPermission.UI
{
    public partial class RegisteredUser : System.Web.UI.Page
    {
        UserPermissionManager userPermissionManager = new UserPermissionManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            

            String strResult;
            String randomKey = Request.QueryString["j_random_key"];
            
            if (randomKey != null && randomKey != "")
            {
                //session clear before login
                //Session.Clear();
                //Session.Abandon();
                //Session.RemoveAll();


                String strURl = "http://172.17.1.181/smartloginapplication/validation.aspx?randomkey=" + randomKey;
                WebResponse objResponse;
                WebRequest objRequest = HttpWebRequest.Create(strURl);
                objResponse = objRequest.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = htmlWeb.Load(strURl);

                HtmlNode UserIdNode = document.GetElementbyId("UserId");
                HtmlNode UserNameNode = document.GetElementbyId("UserName");
                HtmlNode BranchIdNode = document.GetElementbyId("branchID");
                HtmlNode BranchNameNode = document.GetElementbyId("BranchName");

                Session["USER_ID"] = UserIdNode.InnerText.Trim();
                if (Session["USER_ID"].ToString() == "" || Session["USER_ID"] == null){ 
                    Response.Redirect("LogOut.aspx");
                }
                else
                {
                    Response.Redirect("LoggedUserUI.aspx");
                }
                
               
            }
            else
            {
                if (Session["USER_ID"] != null)
                {
                    Response.Redirect("LoggedUserUI.aspx");
                }
                else
                {
                    Response.Redirect(LoginLink.UltimasLogin, true); 
                }
            }
                
        }
    }
}