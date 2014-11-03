using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace EducationalProject.Models
{
    public class Lecture
    {
        public int LectureId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string CreatedOn { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public string PresentationLink { get; set; }
        public string PdfLink { get; set; }
    }
}