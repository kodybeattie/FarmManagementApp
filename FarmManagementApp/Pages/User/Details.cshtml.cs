#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmManagementApp.Models;

namespace FarmManagementApp.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;

        public DetailsModel(FarmManagementApp.Models.FarmContext context)
        {
            _context = context;
        }

        public FarmManagementApp.Models.User User { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.Users.FirstOrDefaultAsync(m => m.Guid == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
