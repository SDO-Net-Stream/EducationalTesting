using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class QuestionAttribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }

        public int AttributeID { get; set; }

        public string Value { get; set; }
    
        public virtual CustomAttribute Attributes { get; set; }

        public virtual Question Questions { get; set; }
    }
}
