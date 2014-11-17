using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EduTesting.WebRequestParameters
{
    public class HttpContextProvider : IHttpContextProvider
    {
        public HttpContext Current
        {
            get { return HttpContext.Current; }
        }
    }
}
