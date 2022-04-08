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
    public class DeleteModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;

        public DeleteModel(FarmManagementApp.Models.FarmContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FarmManagementApp.Models.UserProfile UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile = await _context.UserProfiles
                .Include(u => u.RoleGu)
                .Include(u => u.UserGu).FirstOrDefaultAsync(m => m.UserGuid == id);

            if (UserProfile == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile = await _context.UserProfiles.FindAsync(id);

            if (UserProfile != null)
            {
                _context.UserProfiles.Remove(UserProfile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
