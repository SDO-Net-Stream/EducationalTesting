using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EducationalProject.DataInfo;
using EducationalProject.Models;
using PagedList;

namespace EducationalProject.Controllers
{
    public class HomeController : Controller
    {
        EducationalProjectContext db = new EducationalProjectContext();
        public ActionResult RolePermissions()
        {
            var roleName = Roles.GetRolesForUser().FirstOrDefault();
            if (roleName != null)
            {
                var roleType =
                    (RoleType) Enum.Parse(typeof (RoleType), roleName);
                switch (roleType)
                {
                    case RoleType.Administrator:
                    {
                        return RedirectToAction("GetUsers", "Admin");
                    }
                    case RoleType.Teacher:
                    {
                        return RedirectToAction("TeacherSpace", "Teacher");
                    }
                    case RoleType.User:
                    {
                         return RedirectToAction("Tests", "Test");
                    }
                    case RoleType.None:
                    {
                        return RedirectToAction("NoneSpace", "None");
                    }
                    default:
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            var contacts = db.Contacts.ToList();
            ViewBag.Message = "Your contact page.";
            return View(contacts);
        }

        public ActionResult Literature(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;



            var data = db.BookSections.ToList();
            var books = db.Books;

            var q =
                from e in data
                from et in db.Books.ToList()
                where e.BookSectionId == et.BookSectionId
                select
                    new BooksWrapper()
                    {
                        Name = et.Name,
                        Author = et.Author,
                        BookSection = e.Name,
                        Description = et.Description,
                        BookId = et.BookId
                    };
            q = q.ToList();


            if (!String.IsNullOrEmpty(searchString))
            {
                q = q.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                       || s.Author.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    q = q.OrderByDescending(s => s.Name);
                    break;
                //case "Date":
                //    students = students.OrderBy(s => s.EnrollmentDate);
                //    break;
                //case "date_desc":
                //    students = students.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:  // Name ascending 
                    q = q.OrderBy(s => s.BookSection);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(q.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Lectures()
        {
            var lectures = db.Lectures.ToList();
            ViewBag.Message = "Your contact page.";
            return View(lectures);
        }
        public class BooksWrapper
        {
            public string Name { get; set; }
            public string Author { get; set; }
            public string Description { get; set; }
            public string BookSection { get; set; }
            public int BookId { get; set; }
            //   public List<string> Books { get; set; }
        }
        public JsonResult GetJson()
        {

            var data = db.BookSections.ToList();
            var books = db.Books;

            var q =
                from e in data
                from et in db.Books.ToList()
                where e.BookSectionId == et.BookSectionId
                select
                    new BooksWrapper()
                    {
                        Name = et.Name,
                        Author = et.Author,
                        BookSection = e.Name,
                        Description = et.Description,
                        BookId = et.BookId
                    };
            q = q.ToList();

            var json = Json(q, JsonRequestBehavior.AllowGet);


            return json;
        }



        // GET: /Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = db.BookSections.ToList();
            var books = db.Books;
            var tmp =
                from e in data
                from et in db.Books.ToList()
                where et.BookId == id
                where e.BookSectionId == et.BookSectionId
                select
                    new Book()
                    {
                        Name = et.Name,
                        Author = et.Author,
                        Description = et.Description,
                        BookId = et.BookId
                    };
            tmp = tmp.ToList();
            Book book = tmp.ElementAt(0);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Author, Name, Description, BookSectionId")]Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dbbook = db.Books.FirstOrDefault(b => b.BookId == book.BookId);
                    dbbook.Author = book.Author;
                    dbbook.BookSectionId = book.BookSectionId;
                    dbbook.Name = book.Name;
                    dbbook.Description = book.Description;
                    db.SaveChanges();
                    return RedirectToAction("Literature");
                }
            }
            catch (Exception ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", ex.InnerException.InnerException.Message);
                //ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(book);
        }


        // GET: /Student/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Book book = db.Books.Find(id);
                db.Books.Remove(book);
                db.SaveChanges();
            }
            catch (Exception ex/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Literature");
        }

        // GET: /Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Author, Name, Description, BookSectionId")]Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Literature");
                }
            }
            catch (Exception ex /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", ex.Message);
                //ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(book);
        }
    }
}
