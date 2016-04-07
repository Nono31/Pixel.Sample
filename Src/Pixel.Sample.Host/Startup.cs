using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pixel.Sample.Host.Startup))]
namespace Pixel.Sample.Host
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
