using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyVeryFirstCarSite.Startup))]
namespace MyVeryFirstCarSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
