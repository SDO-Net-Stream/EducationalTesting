using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationalProject.Models
{
    public class BookSection
    {
        public BookSection()
        {
            BookList = new List<Book>();
        }
        public int BookSectionId { get; set; }
        public string  Name { get; set; }
        public virtual  ICollection<Book> BookList { get; set; }
    }
}