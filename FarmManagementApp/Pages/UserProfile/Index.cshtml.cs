#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmManagementApp.Models;

namespace FarmManagementApp.Pages.UserProfile
{
    public class IndexModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;

        public IndexModel(FarmManagementApp.Models.FarmContext context)
        {
            _context = context;
        }

        public IList<FarmManagementApp.Models.UserProfile> UserProfile { get;set; }

        public async Task OnGetAsync()
        {
            UserProfile = await _context.UserProfiles
                .Include(u => u.RoleGu)
                .Include(u => u.UserGu).ToListAsync();
        }
    }
}
