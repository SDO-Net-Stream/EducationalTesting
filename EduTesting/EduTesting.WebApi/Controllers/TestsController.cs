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
    public HttpResponseMessage Post([FromBody] Test test)
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
    public HttpResponseMessage Put(int id, [FromBody] Test test)
    {
      var status = testService.UpdateTest(test);
      if (status) return new HttpResponseMessage(HttpStatusCode.OK);
      throw new HttpResponseException(HttpStatusCode.NotFound);
    }

    // DELETE api/Tests/5
    public HttpResponseMessage Delete(int id)
    {
      var status = testService.DeleteTest(id);
      if (status) return new HttpResponseMessage(HttpStatusCode.OK);
      throw new HttpResponseException(HttpStatusCode.NotFound);
    }

    [HttpGet]
    public IEnumerable<IQuestion> Questions(int testId)
    {
      var questions = testService.GetQuestions(testId);
      if (questions == null)

        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
      return questions;
    }
  }
}
