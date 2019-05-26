using GrantPermission.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GrantPermission.BLL
{
    public class ModuleManager
    {
        private ModuleGateway moduleGateway = new ModuleGateway();
        public DataTable GetAllModule()
        {
            return moduleGateway.GetAllModule();
        }
    }
}