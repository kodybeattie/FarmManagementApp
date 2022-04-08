#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FarmManagementApp.Models;

namespace FarmManagementApp.Pages.UserProfile
{
    public class CreateModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;

        public CreateModel(FarmManagementApp.Models.FarmContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RoleGuid"] = new SelectList(_context.Roles, "Guid", "Guid");
        ViewData["UserGuid"] = new SelectList(_context.Users, "Guid", "Guid");
            return Page();
        }

        [BindProperty]
        public FarmManagementApp.Models.UserProfile UserProfile { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.UserProfiles.Add(UserProfile);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
