using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_63131920.Startup))]
namespace Project_63131920
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
