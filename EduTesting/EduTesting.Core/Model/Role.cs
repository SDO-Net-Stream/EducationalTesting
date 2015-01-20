using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class Role
    {
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public UserRole RoleID { get; set; }
        public string RoleName { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
    }
    public enum UserRole
    {
        User = 1,
        Teacher = 2,
        Administrator = 3
    }
}
