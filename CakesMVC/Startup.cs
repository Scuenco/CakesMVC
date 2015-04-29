using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CakesMVC.Startup))]
namespace CakesMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
