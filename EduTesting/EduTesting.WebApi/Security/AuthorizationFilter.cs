using Abp.Dependency;
using EduTesting.Model;
using EduTesting.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EduTesting.Security
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly IIocManager _iocManager;
        private readonly RoleCode _role;
        public AuthorizationFilter(RoleCode role, IIocManager iocManager)
        {
            _role = role;
            _iocManager = iocManager;
        }
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var userManager = _iocManager.Resolve<IWebUserManager>();
            var user = userManager.CurrentUser;
            if (user != null && user.Roles.Any(r => r.RoleID == (int)_role)                )
                return continuation();
            return Task.FromResult(NewUnauthorized());
        }

        private HttpResponseMessage NewUnauthorized()
        {
            return new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

        public bool AllowMultiple
        {
            get { return true; }
        }
    }
}
