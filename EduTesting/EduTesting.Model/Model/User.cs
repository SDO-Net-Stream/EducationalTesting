using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class User
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserID { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string UserEmail { get; set; }

		public DateTime? DateCreated { get; set; }

		public bool Activated { get; set; }

		public bool Deleted { get; set; }

		public string DomainName { get; set; }

		public string Password { get; set; }

		public string PasswordSalt { get; set; }

		public string PasswordVerificationToken { get; set; }
    
		public virtual ICollection<TestsResult> TestsResults { get; set; }

		public virtual ICollection<UserGroup> UserGroups { get; set; }

		public virtual ICollection<Role> Roles { get; set; }
    }
}
