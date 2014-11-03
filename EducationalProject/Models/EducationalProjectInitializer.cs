using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EducationalProject.Models
{
    public class EducationalProjectInitializer : DropCreateDatabaseAlways<EducationalProjectContext>
    {
        protected override void Seed(EducationalProjectContext context)
        {

            //CONTACTs
            var contacts = new List<Contact>
            {
                new Contact {Address = "7 Naukova St., Building G <br/> Lviv 79060, Ukraine", Fax = "+380 32 244-7002", Map = "", OfficeName = "ELEKS Global Headquarters</br>ELEKS, Ltd.", Phone1 = "+380 32 297-1251 ", Phone2 = "phone2"},
                new Contact {Address = "701 North Green Valley Pkwy</br>Suite 200</br>Henderson, NV 89074",Fax = "+1 678 905-9508", Map = "map", OfficeName = "US Offices</br>ELEKS, Inc. − Headquarters", Phone1 = "+1 866 588-0113 (toll-free US only)", Phone2 = "+1 617 600-4059"},
                new Contact {Address = "15 New England Executive Park</br>Burlington, MA 01803 ",Fax = "+1 678 905-9508", Map = "", OfficeName = "ELEKS, Inc. − Sales Office", Phone1 = "+1 866 588-0113 (toll-free US only)", Phone2 = "+1 617 600-4059"},
                new Contact {Address = "5 Harbour Exchange</br>South Quay</br>London, E14 9GE",Fax = "", Map = "", OfficeName = "UK Office</br>ELEKS Software UK, Ltd.", Phone1 = "+44 203 318-1274", Phone2 = ""},
                new Contact {Address = "Sales Representative</br>Montreal, Canada",Fax = "", Map = "", OfficeName = "ELEKS in Canada", Phone1 = "+14388000584", Phone2 = ""}
            };

            foreach (var contact in contacts)
            {
                context.Contacts.Add(contact);
            }

            //BookSections
            var sections = new List<BookSection>
            {
                new BookSection {Name = "For all"},
                new BookSection {Name = "C#"},
                new BookSection {Name = "C"},
                new BookSection {Name = "C++"},
                new BookSection {Name = "Java"},
                new BookSection {Name = "Javascript"},
                new BookSection {Name = "QA/Testing"}
            };


            //BOOKs
            var books = new List<Book>
            {
                new Book()
                {
                    Name = "Applied Microsoft .NET Framework Programming",
                    Author = "Jeffrey Richter",
                    Description = "",
                    BookSection = sections.ElementAt(1),
                    Link = ""
                },
                new Book()
                {
                    Name = "The C Programming Language",
                    Author = "Kernighan and Dennis Ritchie",
                    Description = "",
                    BookSection = sections.ElementAt(2),
                    Link = "http://www.amazon.com/Windows-via-Jeffrey-M-Richter/dp/0735624240/"
                },
                new Book()
                {
                    Name = "Essential C++",
                    Author = "Stanley B. Lippman",
                    Description = "",
                    BookSection = sections.ElementAt(3),
                    Link = ""
                },
                new Book()
                {
                    Name = "Windows via C/C++",
                    Author = "Jeffrey Richter",
                    Description = "",
                    BookSection = sections.ElementAt(3),
                    Link = "http://www.amazon.com/The-Programming-Language-2nd-Edition/dp/0131103628"
                }
            };

            foreach (var section in sections)
            {
                context.BookSections.Add(section);
            }

            foreach (var book in books)
            {
                context.Books.Add(book);
            }


            //Lectures
            var lectures = new List<Lecture>
            {
                new Lecture()
                {
                    Name = "ASP.NET MVC 5 Tutorial - Step by Step ",
                    Author = "Patrick WashingtonDC",
                    Description = "Easy to follow ASP.NET MVC and Entity Framework tutorial.",
                    CreatedOn = "17 трав. 2014",
                    VideoLink = "https://www.youtube.com/watch?v=VAtVv1Q7ufM&list=WL&index=116",
                    PdfLink = "",
                    PresentationLink = ""
                },
                new Lecture()
                {
                    Name = "ASP.NET Web Pages, Web Forms, and MVC - 14 Roles and Permissions ",
                    Author = "Tuts+",
                    Description = "học asp.net MVC với webmatrix và visual studio",
                    CreatedOn = "20 квіт. 2014",
                    VideoLink = "https://www.youtube.com/watch?v=JFSIiSWH_J0&list=WL&index=117",
                    PdfLink = "",
                    PresentationLink = ""
                },
                new Lecture()
                {
                    Name = "ASP.NET MVC 5 Tutorial - Step by Step ",
                    Author = "Patrick WashingtonDC",
                    Description = "Easy to follow ASP.NET MVC and Entity Framework tutorial.",
                    CreatedOn = "17 трав. 2014",
                    VideoLink = "https://www.youtube.com/watch?v=VAtVv1Q7ufM&list=WL&index=116",
                    PdfLink = "",
                    PresentationLink = ""
                },
            };

            foreach (var lecture in lectures)
            {
                context.Lectures.Add(lecture);
            }

            context.SaveChanges();
        }
    }
}