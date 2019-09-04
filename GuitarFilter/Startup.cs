using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuitarFilter.Startup))]
namespace GuitarFilter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
