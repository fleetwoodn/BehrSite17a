using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BehrSite17.Startup))]
namespace BehrSite17
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
