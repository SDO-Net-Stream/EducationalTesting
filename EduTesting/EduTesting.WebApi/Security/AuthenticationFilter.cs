using Abp.Dependency;
using EduTesting.DataProvider;
using EduTesting.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Security;
namespace EduTesting.Security
{
    public class AuthenticationFilter : IAuthenticationFilter
    {
        private readonly IIocManager _iocManager;
        public AuthenticationFilter(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {

            var httpContext = context.Request.Properties["MS_HttpContext"] as HttpContextWrapper;
            var cookie = httpContext.Request.Cookies.Get(EduTestingConsts.AUTH_COOKIE_NAME);
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var userId = ticket.UserData;
                var userManager = _iocManager.Resolve<IWebUserManager>();
                var userProvider = _iocManager.Resolve<IUserProvider>();
                var user = userProvider.GetUserByDomainName(userId);
                userManager.SetCurrent(user);
            }
            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}
