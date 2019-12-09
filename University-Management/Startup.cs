using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(University_Management.Startup))]
namespace University_Management
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
