using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class QuestionAttribute
    {
		[Key, Column(Order = 0)]
        public int QuestionID { get; set; }
        [Key, Column(Order = 1)]
        public int AttributeID { get; set; }
        public string Value { get; set; }
    
        public virtual CustomAttribute Attribute { get; set; }
        public virtual Question Question { get; set; }
    }
}
