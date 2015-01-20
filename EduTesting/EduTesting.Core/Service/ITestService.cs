using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Model;
using EduTesting.ViewModels.Test;

namespace EduTesting.Service
{
    public interface ITestService
    {
        TestViewModel GetTest(TestListItemViewModel test);
        TestListItemViewModel[] GetTests(TestListFilterViewModel filter);

        TestViewModel InsertTest(TestViewModel test);
        void UpdateTest(TestViewModel test);
        void DeleteTest(TestViewModel test);
    }
}
