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
            var userManager = _iocManager.Resolve<IWebUserManager>();
            if (userManager.CurrentUser == null)
            {
                var session = _iocManager.Resolve<ISessionManager>();
                var user = session.GetUserFromSession();
                if (user != null && user.UserIsActive)
                {
                    userManager.SetCurrent(user);
                }
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
