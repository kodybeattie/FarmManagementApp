using System;
using System.Collections.Generic;

namespace FarmManagementApp.Models
{
    public partial class Farm
    {
        public Farm()
        {
            Users = new HashSet<User>();
        }

        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public Guid? HeadManager { get; set; }
        public string? StateProv { get; set; }
        public string? Country { get; set; }
        public string? PostalZipCode { get; set; }
        public string? MainPhone { get; set; }
        public string? MainEmail { get; set; }

        public virtual User? HeadManagerNavigation { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
