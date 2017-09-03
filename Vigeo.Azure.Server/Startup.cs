using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(VigBE.Startup))]

namespace VigBE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}