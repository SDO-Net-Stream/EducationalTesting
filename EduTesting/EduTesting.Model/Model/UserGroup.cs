using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class UserGroup
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int GroupID { get; set; }

		public string GroupName { get; set; }

		public virtual ICollection<Test> Tests { get; set; }

		public virtual ICollection<User> Users { get; set; }
    }
}
