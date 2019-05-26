using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GrantPermission.Startup))]
namespace GrantPermission
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
