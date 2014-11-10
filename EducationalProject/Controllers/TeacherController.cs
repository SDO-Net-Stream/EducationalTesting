using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using EducationalProject.Controllers.Utilities;
using EducationalProject.Models;
using WebMatrix.WebData;

namespace EducationalProject.Controllers
{
    public class TeacherController : Controller
    {
        [Authorize(Roles = "Teacher")]
        public ActionResult TeacherSpace()
        {
            var testWrappers = new List<TestWrapper>();
            using (var db = new UsersContext())
            {
                var userId = WebSecurity.GetUserId(User.Identity.Name);
                var testList =
                    db.Tests.Where(test => test.User.UserId == userId)
                        .OrderByDescending(date => date.DateDownload)
                        .ToList();

                testWrappers.AddRange(testList.Select(test => new TestWrapper
                {
                    TestId = test.TestId, TestName = test.TestName, DateDownload = test.DateDownload, Order = test.Order
                }));
            }
            return View(testWrappers);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult FileUpload()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var directory = Server.MapPath("~/TestFiles/User=" + userId);
            CreateIfMissing(directory);
            for (var index = 0; index < Request.Files.Count; ++index)
            {
                var file = Request.Files[index];
                if (file != null && file.ContentLength > 0 && file.ContentType == "text/xml")
                {   
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(directory, fileName);
                    file.SaveAs(path);
                    using (var db = new UsersContext())
                    {
                        try
                        {
                            var test = XmlToTestParser.ParseFromXml(path);
                            test.User = db.UserProfiles.FirstOrDefault(u => u.UserId == userId);
                            test.PathFile = path;
                            db.Tests.Add(test);
                            db.SaveChanges();
                        }
                        catch (Exception)
                        {
                            return RedirectToAction("TeacherSpace", "Teacher");
                        }
                    }
                }
            }
            return RedirectToAction("TeacherSpace", "Teacher");
        }

        private static void CreateIfMissing(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteTest(int id)
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var test = db.Tests.FirstOrDefault(t => t.TestId == id);
                    db.Tests.Attach(test);
                    var listQuestion = test.Questions.ToList();
                    foreach (var question in listQuestion)
                    {
                        if (question is QuestionWithVariants)
                        {
                            var listVariant = ((QuestionWithVariants) question).VariantAnswers.ToList();
                            foreach (var variantAnswer in listVariant)
                            {
                                db.VariantAnswers.Remove(variantAnswer);
                            }
                            db.QuestionWithVariants.Remove((QuestionWithVariants) question);
                        }
                        else
                        {
                            db.Questions.Remove(question);
                        }
                    }
                    db.Tests.Remove(test);
                    db.SaveChanges();
                }
                return RedirectToAction("TeacherSpace", "Teacher");
            }
            catch (Exception)
            {
                return RedirectToAction("TeacherSpace", "Teacher");
            }
        }
    }
}