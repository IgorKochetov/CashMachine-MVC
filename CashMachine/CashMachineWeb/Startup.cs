using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CashMachineWeb.Startup))]
namespace CashMachineWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
