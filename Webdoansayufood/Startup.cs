using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Webdoansayufood.Startup))]
namespace Webdoansayufood
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
