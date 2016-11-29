using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Irci.Startup))]
namespace Irci
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
