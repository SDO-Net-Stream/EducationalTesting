using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationalProject.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public int BookSectionId { get; set; }
        public virtual BookSection BookSection { get; set; }
    }
}