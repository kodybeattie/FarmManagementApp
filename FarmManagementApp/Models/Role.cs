using System;
using System.Collections.Generic;

namespace FarmManagementApp.Models
{
    public partial class Role
    {
        public Role()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public Guid Guid { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
