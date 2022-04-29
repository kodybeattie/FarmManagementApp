#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FarmManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmManagementApp.Pages.Auth
{
    public class PasswordResetModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;

        public PasswordResetModel(FarmManagementApp.Models.FarmContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FarmManagementApp.Models.User User { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Password == ConfirmPassword) {
                string queryString = HttpContext.Request.QueryString.Value;
                string currEmail = queryString.Replace("?u=", "");
                Models.User foundUser  = _context.Users.FirstOrDefault(u => u.Email == currEmail);
                foundUser.Password = User.Password;
                _context.Attach(foundUser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            return RedirectToPage("../Index");
        }
    }
}
