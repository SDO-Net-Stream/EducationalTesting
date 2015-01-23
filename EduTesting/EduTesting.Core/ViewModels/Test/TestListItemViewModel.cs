using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.Test
{
    public class TestListItemViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public TestStatus TestStatus { get; set; }
        public int UserFirstName { get; set; }
        public int UserLastName { get; set; }
    }
}
