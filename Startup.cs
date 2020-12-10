using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CompShop2.Startup))]
namespace CompShop2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
