using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BusinessInsights.Startup))]
namespace BusinessInsights
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
