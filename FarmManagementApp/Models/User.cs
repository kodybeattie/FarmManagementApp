using System;
using System.Collections.Generic;

namespace FarmManagementApp.Models
{
    public partial class User
    {
        public User()
        {
            Farms = new HashSet<Farm>();
        }

        public Guid Guid { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid? FarmGuid { get; set; }

        public virtual Farm? FarmGu { get; set; }
        public virtual UserProfile UserProfile { get; set; } = null!;
        public virtual ICollection<Farm> Farms { get; set; }
    }
}
