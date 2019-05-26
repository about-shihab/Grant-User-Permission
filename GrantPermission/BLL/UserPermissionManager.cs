using GrantPermission.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GrantPermission.BLL
{
    public class UserPermissionManager
    {
        UserPermissionGateway userPermissionGateway = new UserPermissionGateway();
        public string sp_GrantPermisson(string pgrant_user_id, int pmodule_id, int? prole_id, string puser_id,int active_status)
        {
            return userPermissionGateway.sp_GrantPermisson(pgrant_user_id, pmodule_id, prole_id, puser_id, active_status);
        }

        public DataTable GetAllGrantUser()
        {
            return userPermissionGateway.GetAllGrantUser();
        }
        public DataTable GetAllPendingUser()
        {
            return userPermissionGateway.GetAllPendingUser();
        }
        public DataTable GetUserBasicInfo(string puser_id)
        {
            return userPermissionGateway.GetUserBasicInfo(puser_id);
        }
        public DataTable GetUserBasicInfo(string puser_id,int moduleId)
        {
            return userPermissionGateway.GetUserBasicInfo(puser_id,moduleId);
        }
        public DataTable GetAllRequestByStatus(int activeStatus)
        {
            return userPermissionGateway.GetAllRequestByStatus(activeStatus);
        }

       public bool CheckGrantAdminUser(string userId)
        {
            return userPermissionGateway.CheckGrantAdminUser(userId);
        }
    }
}