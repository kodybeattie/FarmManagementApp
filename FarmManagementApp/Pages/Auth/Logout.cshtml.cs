using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FarmManagementApp.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;

        public LogoutModel(FarmManagementApp.Models.FarmContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            HttpContext.Session.SetString("_email", "");
            HttpContext.Session.SetInt32("_logged_in", 0);
            HttpContext.Session.SetString("_guid","");

            return RedirectToPage("../Index");
        }

        [BindProperty]
        public FarmManagementApp.Models.User User { get; set; }
    }
}
