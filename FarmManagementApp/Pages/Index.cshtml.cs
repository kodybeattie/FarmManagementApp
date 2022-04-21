using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FarmManagementApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetInt32("_logged_in") == 1) {
                return Page();
            } else {
                return RedirectToPage("/Farm/FarmSelect");
            }
        }
    }
}