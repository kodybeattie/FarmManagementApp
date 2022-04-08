#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FarmManagementApp.Models;

namespace FarmManagementApp.Pages.Farm
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
        ViewData["HeadManager"] = new SelectList(_context.Users, "Guid", "Guid");
            return Page();
        }

        [BindProperty]
        public Models.Farm Farm { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Farms.Add(Farm);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
