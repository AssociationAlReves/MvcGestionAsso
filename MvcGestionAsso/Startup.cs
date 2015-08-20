using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcGestionAsso.Startup))]
namespace MvcGestionAsso
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
