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
        public int UserId { get; set; }
        public Model.Question[] Questions { get; set; }
    }
}
