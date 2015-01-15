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
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public Nullable<System.DateTime> UserDateCreated { get; set; }
        public bool UserActivated { get; set; }
        public bool UserDeleted { get; set; }
        public string UserDomainName { get; set; }
        public string UserPassword { get; set; }
        public string UserPasswordSalt { get; set; }
        public string UserPasswordVerificationToken { get; set; }
    
        public virtual ICollection<TestResult> TestsResults { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
