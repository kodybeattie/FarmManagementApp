#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmManagementApp.Models;

namespace FarmManagementApp.Pages.Role
{
    public class IndexModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;

        public IndexModel(FarmManagementApp.Models.FarmContext context)
        {
            _context = context;
        }

        public IList<FarmManagementApp.Models.Role> Role { get;set; }

        public async Task OnGetAsync()
        {
            Role = await _context.Roles.ToListAsync();
        }
    }
}
