using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EduTesting.Model;
using EduTesting.Service;
using EduTesting.ViewModels.Test;

namespace EduTesting.Controllers
{
  public class TestController : ApiController
  {
    private TestService testService;

    public TestController()
    {
    }

    // GET api/Tests
    public HttpResponseMessage Get()
    {
      var tests = testService.GetTests();
      if (tests == null) throw new HttpResponseException(HttpStatusCode.NotFound);
      return Request.CreateResponse(HttpStatusCode.OK, tests);
    }

    // GET api/Tests/5
    public HttpResponseMessage Get(int id)
    {
      var test = testService.GetTest(id);
      if (test == null) throw new HttpResponseException(HttpStatusCode.NotFound);
      return Request.CreateResponse(HttpStatusCode.OK, test);
    }

    // POST api/Tests
    public HttpResponseMessage Post([FromBody] TestListItemViewModel test)
    {
      var newTest = testService.InsertTest(test);
      if (newTest != null)
      {
        var msg = new HttpResponseMessage(HttpStatusCode.Created);
        //msg.Headers.Location = new Uri(Request.RequestUri + newTest.TestId.ToString());
        return msg;
      }
      throw new HttpResponseException(HttpStatusCode.Conflict);
    }

    // PUT api/Tests/5
    public HttpResponseMessage Put(int id, [FromBody] TestListItemViewModel test)
    {
        try
        {
            testService.UpdateTest(test);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        catch (BusinessLogicException)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }

    // DELETE api/Tests/5
    public HttpResponseMessage Delete(int id)
    {
        try
        {
            testService.DeleteTest(new ViewModels.Test.TestListItemViewModel { TestId = id });
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        catch (BusinessLogicException)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }

    //[HttpGet]
    //public IEnumerable<Question> Questions(int testId)
    //{
    //  var questions = testService.GetQuestions(testId);
    //  if (questions == null)
    //    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
    //  return questions;
    //}
  }
}
