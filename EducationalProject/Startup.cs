using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EducationalProject.Startup))]
namespace EducationalProject
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}