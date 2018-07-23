using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BillingWeb.Startup))]
namespace BillingWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
