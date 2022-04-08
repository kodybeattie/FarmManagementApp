#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FarmManagementApp.Models;

namespace FarmManagementApp.Pages.User
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
            return Page();
        }

        [BindProperty]
        public FarmManagementApp.Models.User User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            Models.User currUser = _context.Users.FirstOrDefault(u => u.Email == User.Email);
            Models.UserProfile profile = new Models.UserProfile();
            profile.UserGuid = currUser.Guid;
            profile.FirstName = "";
            profile.LastName = "";
            profile.RoleGuid = _context.Roles.FirstOrDefault(u => u.Name == "User").Guid;
            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("_email", currUser.Email);
            HttpContext.Session.SetString("_guid", currUser.Guid.ToString());
            HttpContext.Session.SetInt32("_logged_in", 1);

            return RedirectToPage("../UserProfile/Edit");
        }
    }
}
