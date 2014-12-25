using System.Web;
using System.Web.Mvc;

namespace EduTesting.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Elmah.Contrib.Mvc.ElmahHandleErrorAttribute());
        }
    }
}