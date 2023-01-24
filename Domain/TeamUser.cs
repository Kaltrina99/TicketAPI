using System;
using System.Collections.Generic;

namespace Domain
{
    public class TeamUser
    {
        public string UserId { get; set; }
        public Guid TeamId { get; set; }

        public virtual ICollection<AppUser> User { get; set; }
        public virtual ICollection<Team> Team { get; set; }
    }
}