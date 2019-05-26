using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrantPermission.BLL
{
    public class MasterManager
    {
        public ModuleManager moduleManager;
        public RoleManager roleManager;
        public MasterManager()
        {
            ModuleManager moduleManager = new ModuleManager();
            RoleManager roleManager = new RoleManager();
        }
    }
}