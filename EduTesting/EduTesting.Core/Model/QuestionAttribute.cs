using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class QuestionAttribute
    {
<<<<<<< HEAD:EduTesting/EduTesting.Core/Model/QuestionAttribute.cs
		[Key, Column(Order = 0)]
        public int QuestionID { get; set; }
        [Key, Column(Order = 1)]
        public int AttributeID { get; set; }
        public string Value { get; set; }
    
        public virtual CustomAttribute Attribute { get; set; }
        public virtual Question Question { get; set; }
=======
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int QuestionID { get; set; }

		public int AttributeID { get; set; }

		public string Value { get; set; }
    
		public virtual CustomAttribute Attributes { get; set; }

		public virtual Question Questions { get; set; }
>>>>>>> 92242cc6867a61bb2fd17d0fdf647e5e3794ac6c:EduTesting/EduTesting.Model/Model/QuestionAttribute.cs
    }
}
