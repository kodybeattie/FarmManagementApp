using System;
using System.Collections.Generic;

namespace FarmManagementApp.Models
{
    public partial class User
    {
        public Guid Guid { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual UserProfile UserProfile { get; set; } = null!;
    }
}
