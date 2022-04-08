#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmManagementApp.Models;

namespace FarmManagementApp.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;

        public LoginModel(FarmManagementApp.Models.FarmContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        [BindProperty]
        public FarmManagementApp.Models.User User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            bool isUser = _context.Users.Any(m => m.Email == User.Email && m.Password == User.Password);

            if (isUser) {
                Models.User findUser = _context.Users.FirstOrDefault(m => m.Email == User.Email && m.Password == User.Password);
                HttpContext.Session.SetString("_email",findUser.Email);
                HttpContext.Session.SetString("_guid", findUser.Guid.ToString());
                HttpContext.Session.SetInt32("_logged_in", 1);
            }
            else {
                return Page();
            }

            return RedirectToPage("../Index");
        }
    }
}
