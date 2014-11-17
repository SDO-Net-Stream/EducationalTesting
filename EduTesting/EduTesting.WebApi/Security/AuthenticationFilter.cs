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
        private readonly IWebUserManager _webUserManager;
        private readonly ISessionManager _sessionManager;
        public AuthenticationFilter(IWebUserManager webUserManager, ISessionManager sessionManager)
        {
            _webUserManager = webUserManager;
            _sessionManager = sessionManager;
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (_webUserManager.CurrentUser == null)
            {
                var user = _sessionManager.GetUserFromSession();
                if (user != null)
                {
                    _webUserManager.SetCurrent(user);
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
