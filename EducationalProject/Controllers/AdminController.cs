using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using EducationalProject.Filters;
using EducationalProject.Models;
using PagedList;
using WebMatrix.WebData;

namespace EducationalProject.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminSpace()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public ViewResult Indexer(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var db = new UsersContext();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //  ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.UserProfiles
                           select s;


            var users = db.UserProfiles.ToList();

            var allRAU = from e in users
                         select new { Name = e.UserName, Roless = Roles.GetRolesForUser(e.UserName).ElementAt(0) };

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.UserName.ToUpper().Contains(searchString.ToUpper())
                                       || s.UserName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.UserName);
                    break;
                default:
                    students = students.OrderBy(s => s.UserName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }
        //GOLOVCHUK

        //ADDING ROLES TO BD
        //GET
        [Authorize(Roles = "Administrator")]
        public ActionResult RoleCreate()
        {
            return View();
        }
        //POST
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleCreate(string RoleName)
        {

            if (!Roles.RoleExists(RoleName))
            {
                Roles.CreateRole(Request.Form["RoleName"]);
            }
            else
            {
                ViewBag.ResultMessage = "Role already exists !";
                return View();
            }

            return RedirectToAction("RoleIndex", "Admin");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddRoleToUser(int id)
        {
            //using (var db = new UsersContext())
            //{
            //    var test = db.Tests.FirstOrDefault(t => t.TestId == id);
            //    db.Tests.Attach(test);
            //    var listQuestion = test.Questions.ToList();
            //    foreach (var question in listQuestion)
            //    {
            //        if (question is QuestionWithVariants)
            //        {
            //            var listVariant = ((QuestionWithVariants)question).VariantAnswers.ToList();
            //            foreach (var variantAnswer in listVariant)
            //            {
            //                db.VariantAnswers.Remove(variantAnswer);
            //            }
            //            db.QuestionWithVariants.Remove((QuestionWithVariants)question);
            //        }
            //        else
            //        {
            //            db.Questions.Remove(question);
            //        }
            //    }
            //    db.Tests.Remove(test);
            //    db.SaveChanges();
            //}
            return RedirectToAction("GetUsers", "Admin");
        }

        //ROLES INDEX
        //GET
        [Authorize(Roles = "Administrator")]
        public ActionResult RoleIndex()
        {
            var roles = Roles.GetAllRoles();
            return View(roles);
        }
        //GET NEW USERS
        [Authorize(Roles = "Administrator")]
        public ActionResult GetUsers()
        {


            //   var roleName = Roles.GetRolesForUser().FirstOrDefault();
            // var db = new UsersContext();
            // var users = db.UserProfiles.ToList();
            var usersWithOutRole = new List<UsersWrapper>();
            var users = Roles.GetUsersInRole("None");
            //  if (roleName == null)
            //{

            foreach (var user in users)
            {

                usersWithOutRole.Add(new UsersWrapper
                {
                    UserName = user
                });
            }
            //}
            var roles = Roles.GetAllRoles();

            var query = from role in roles
                        where role != "None"
                        select role;

            SelectList list = new SelectList(query);
            ViewBag.Roles = list;
            ViewBag.Error = "List is Empty!";

            return View(usersWithOutRole);
        }
        //USER LIST
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetUsers(string UserName, string RoleName)
        {
            //var db = new UsersContext();
            //var users = db.UserProfiles.ToList();
            // var users = new UsersWrapper();
            // var usersWithOutRole = new List<UsersWrapper>();

            //foreach (var user in users)
            //{
            //    try
            //    {
            //        Roles.GetRolesForUser(user.UserName);
            //    }
            //    catch (Exception) 
            //    {
            //        usersWithOutRole.Add(new UsersWrapper { 
            //            UserName = user.UserName,
            //            UserId = user.UserId});
            //    }
            //}

            //foreach(var user in users)
            //{
            //    user.UserName = UserName;
            //}

            Roles.AddUserToRole(UserName, RoleName);
            Roles.RemoveUserFromRole(UserName, "None");
            // ViewBag.ResultMessage = "Username added to the role succesfully !";


            SelectList list = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = list;
            return RedirectToAction("GetUsers", "Admin");

        }
        //ALL USERS
        [Authorize(Roles = "Administrator")]
        public ActionResult GetAllUsers()
        {
            var db = new UsersContext();

            var users = db.UserProfiles.ToList();
            //var users = Roles.GetUsersInRole("None");


            //List<string> roleset_to_find = new List<string>() { "Administrator", "Teacher", "User" };

            //List<string> membersFound = new List<string>();

            //foreach (string role in roleset_to_find)
            //{
            //    membersFound.AddRange(Roles.GetUsersInRole(role));
            //}

            var roles = new List<String[]>();

            foreach (var user in users)
            {

                roles.Add(Roles.GetRolesForUser(user.UserName));



            }
            ViewBag.Roles = roles;
            ViewBag.Error = "List is Empty!";
            // var users = Membership.GetAllUsers();
            return View(users);

        }

        //DELETE ROLES
        //GET
        [Authorize(Roles = "Administrator")]
        public ActionResult RoleDelete(string RoleName)
        {
            string[] users = Roles.GetUsersInRole(RoleName);
            if (users.Count() == 0)
            {
                Roles.DeleteRole(RoleName);
            }
            else
            {
                ViewBag.Error = "You can't delete role!";
            }

            return RedirectToAction("RoleIndex", "Admin");
        }

        //DETELE USERS
        [Authorize(Roles = "Administrator")]

        public ActionResult DeleteUser(string UserName)
        {
            //try
            //{
            // TODO: Add delete logic here
            if (Roles.GetRolesForUser(UserName).Count() > 0)
            {
                Roles.RemoveUserFromRoles(UserName, Roles.GetRolesForUser(UserName));
            }
            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(UserName); // deletes record from webpages_Membership table
            ((SimpleMembershipProvider)Membership.Provider).DeleteUser(UserName, true); // deletes record from UserProfile table

            return RedirectToAction("GetAllUsers", "Admin");
            //}
            //catch
            //{
            //    return View(UserName);
            //}
        }

        //EDIT ROLES
        //GET
        [Authorize(Roles = "Administrator")]
        public ActionResult RenameRoleAndUsers(string RoleName)
        {
            ViewData["textbox1"] = RoleName;
            return View();
        }

        //POST
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RenameRoleAndUsers(string OldRoleName, string NewRoleName)
        {
            string[] users = Roles.GetUsersInRole(OldRoleName);
            Roles.CreateRole(NewRoleName);
            if (users.Count() != 0)
            {
                Roles.AddUsersToRole(users, NewRoleName);
                Roles.RemoveUsersFromRole(users, OldRoleName);
            }
            Roles.DeleteRole(OldRoleName);

            return RedirectToAction("RoleIndex", "Admin");
        }

        /// <summary>
        /// CREATE A NEW ROLE TO A USER
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult RoleAddToUser()
        {
            SelectList list = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = list;

            return View();
        }

        /// <summary>
        /// ADD ROLE TO USER
        /// </summary>
        /// <param name="RoleName"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string RoleName, string UserName)
        {

            if (Roles.IsUserInRole(UserName, RoleName))
            {
                ViewBag.ResultMessage = "This user already has the role specified !";
            }
            else
            {
                Roles.AddUserToRole(UserName, RoleName);
                ViewBag.ResultMessage = "Username added to the role succesfully !";
            }

            SelectList list = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = list;
            ViewBag.Error = "No such User!";
            return View();
        }

        /// <summary>
        ///GET ALLL ROLES
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                try
                {

                    ViewBag.RolesForThisUser = Roles.GetRolesForUser(UserName);
                    SelectList list = new SelectList(Roles.GetAllRoles());
                    if (list != null)
                    {
                        ViewBag.Roles = list;
                    }
                    else
                    {
                        ViewBag.Error = "No such User!";
                    }
                }
                catch (Exception)
                {
                    ViewBag.Error = "No such User!";
                }
                //return View("RoleAddToUser");

            }

            return View("RoleAddToUser");
        }



        //DELETE
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {

            if (Roles.IsUserInRole(UserName, RoleName))
            {
                Roles.RemoveUserFromRole(UserName, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            ViewBag.RolesForThisUser = Roles.GetRolesForUser(UserName);
            SelectList list = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = list;
            ViewBag.Error = "No such User!";


            return View("RoleAddToUser");
        }
    }
}