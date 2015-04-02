using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCEmployeeScheduler.Startup))]
namespace MVCEmployeeScheduler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
