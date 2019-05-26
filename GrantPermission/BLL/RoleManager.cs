using GrantPermission.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace GrantPermission.BLL
{
    public class RoleManager
    {
        private RoleGateway roleGateway = new RoleGateway();
        private UserPermissionManager userPermissionManager = new UserPermissionManager();
        public DataTable GetAllRoleByModule(string userId,int moduleId)
        {

            DataTable dtab = userPermissionManager.GetUserBasicInfo(userId,moduleId);
            DataRow row = dtab.Rows[0];
            
            int ho_user_flag = Convert.ToInt32(row["HO_USER_FLAG"]);
            
            if (ho_user_flag==1)
            {
                return roleGateway.GetAllRoleByModule(moduleId,"HO");

            }
            else
            {
                return roleGateway.GetAllRoleByModule(moduleId, "BR");

            }
        }
        public DataTable GetRoleByRoleName(string roleName)
        {
            return roleGateway.GetRoleByRoleName(roleName);
        }
    }
}