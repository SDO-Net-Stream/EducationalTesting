using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class CustomAttribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttributeID { get; set; }

        public string AttributeName { get; set; }
    
        public virtual ICollection<QuestionAttribute> QuestionAttributes { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
