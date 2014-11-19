using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Model
{
    public class BusinessLogicException : UserFriendlyException
    {
        public BusinessLogicException(string message, string details)
            : base(message, details)
        {
        }
        public BusinessLogicException(string message)
            : base(message)
        {
        }

        public BusinessLogicException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
