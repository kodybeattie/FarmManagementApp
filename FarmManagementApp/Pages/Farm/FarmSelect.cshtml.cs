#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmManagementApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarmManagementApp.Pages.Farm
{
    public class FarmSelectModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;

        public FarmSelectModel(FarmManagementApp.Models.FarmContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string selectedFarm { get; set; }

        [BindProperty]
        public FarmManagementApp.Models.User User { get; set; }

        public List<SelectListItem> farmList { get; set; }

        public async Task OnGetAsync()
        {
            farmList = _context.Farms.Select(x => new SelectListItem { 
                Text = x.Name,
                Value = x.Guid.ToString()
            }).ToList();
        }

        public async Task<IActionResult> OnPostAsync() {
            HttpContext.Session.SetString("_farmGuid", selectedFarm);
            bool isUser = _context.Users.Any(m => m.FarmGuid.ToString() == selectedFarm && m.Email == User.Email && m.Password == User.Password);

            if (isUser)
            {
                Models.User findUser = _context.Users.FirstOrDefault(m => m.FarmGuid.ToString() == selectedFarm && m.Email == User.Email && m.Password == User.Password);
                HttpContext.Session.SetString("_email", findUser.Email);
                HttpContext.Session.SetString("_guid", findUser.Guid.ToString());
                HttpContext.Session.SetInt32("_logged_in", 1);
            }
            else
            {
                return Page();
            }

            return RedirectToPage("../Index");
        }
    }
}
