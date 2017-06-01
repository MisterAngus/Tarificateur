using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tarificateur.Startup))]

namespace Tarificateur
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}