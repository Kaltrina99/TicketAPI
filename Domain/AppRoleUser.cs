using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppRoleUser : IdentityUserRole<Guid>
    {
        public string userId { get; set; }
        public string roleId { get; set; }

        public virtual ICollection<AppUser> User { get; set; }
        public virtual ICollection<AppRole> Role { get; set; }
    }
}