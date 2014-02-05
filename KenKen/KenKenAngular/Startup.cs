using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(KenKenAngular.Startup))]

namespace KenKenAngular
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
