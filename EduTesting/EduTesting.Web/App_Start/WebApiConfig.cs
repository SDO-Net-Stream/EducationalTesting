using System.Web;
using System.Web.Http;

namespace EduTesting.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new Elmah.Contrib.WebApi.ElmahHandleErrorApiAttribute());
        }
    }
}