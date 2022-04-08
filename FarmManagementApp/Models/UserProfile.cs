using System;
using System.Collections.Generic;

namespace FarmManagementApp.Models
{
    public partial class UserProfile
    {
        public Guid UserGuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid RoleGuid { get; set; }

        public virtual Role RoleGu { get; set; } = null!;
        public virtual User UserGu { get; set; } = null!;
    }
}
