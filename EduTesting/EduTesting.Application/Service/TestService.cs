using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{

    public class TestService : EduTestingAppServiceBase, ITestService
    {
        public List<string> GetNames()
        {
            return new List<string>
            {
                "first"
            };
        }
    }
}
