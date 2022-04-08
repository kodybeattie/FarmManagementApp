#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmManagementApp.Models;

namespace FarmManagementApp.Pages.Farm
{
    public class DeleteModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;

        public DeleteModel(FarmManagementApp.Models.FarmContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Farm Farm { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Farm = await _context.Farms
                .Include(f => f.HeadManagerNavigation).FirstOrDefaultAsync(m => m.Guid == id);

            if (Farm == null)
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

            Farm = await _context.Farms.FindAsync(id);

            if (Farm != null)
            {
                _context.Farms.Remove(Farm);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
