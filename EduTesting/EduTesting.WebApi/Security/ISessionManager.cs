using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Security
{
    public interface ISessionManager
    {
        void UpdateSession(User user);
        void TerminateSession();
        User GetUserFromSession();
    }
}
