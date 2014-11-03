using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationalProject.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        [AllowHtml]
        public string OfficeName { get; set; }
        [AllowHtml]
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        [AllowHtml]
        public string Map { get; set; }

    }
}