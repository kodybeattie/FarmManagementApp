#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FarmManagementApp.Models;

namespace FarmManagementApp.Pages.UserProfile
{
    public class EditModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;

        public EditModel(FarmManagementApp.Models.FarmContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FarmManagementApp.Models.UserProfile UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                id = new Guid(HttpContext.Session.GetString("_guid"));
            }

            UserProfile = await _context.UserProfiles
                .Include(u => u.RoleGu)
                .Include(u => u.UserGu).FirstOrDefaultAsync(m => m.UserGuid == id);

            if (UserProfile == null)
            {
                return NotFound();
            }
            ViewData["RoleGuid"] = new SelectList(_context.Roles, "Guid", "Guid");
            ViewData["UserGuid"] = new SelectList(_context.Users, "Guid", "Guid");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(UserProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(UserProfile.UserGuid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserProfileExists(Guid id)
        {
            return _context.UserProfiles.Any(e => e.UserGuid == id);
        }
    }
}
