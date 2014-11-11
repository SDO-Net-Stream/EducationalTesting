using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EduTest.Startup))]
namespace EduTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
