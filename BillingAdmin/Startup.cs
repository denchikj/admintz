using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BillingAdmin.Startup))]
namespace BillingAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
