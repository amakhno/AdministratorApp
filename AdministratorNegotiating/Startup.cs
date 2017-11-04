using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdministratorNegotiating.Startup))]
namespace AdministratorNegotiating
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
