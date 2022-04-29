#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FarmManagementApp.Models;
using System.Net.Mail;
using System.Net;

namespace FarmManagementApp.Pages.Auth
{
    public class PasswordResetRequestModel : PageModel
    {
        private readonly FarmManagementApp.Models.FarmContext _context;
        private readonly IConfiguration _config;

        public PasswordResetRequestModel(FarmManagementApp.Models.FarmContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(_config["EmailSettings:EmailAddress"], _config["EmailSettings:SenderName"]);
            message.To.Add(new MailAddress(User.Email));
            message.Subject = "Password Reset Request";
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = "<html><body><p>New Password Reset Request</p><br/><a href=\"https://localhost:7082/Auth/PasswordReset?u="+User.Email+"\">Reset Password</a></body></html>";
            smtp.Port = Int32.Parse(_config["EmailSettings:Port"]);
            smtp.Host = _config["EmailSettings:Host"]; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_config["EmailSettings:EmailAddress"], _config["EmailSettings:Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

            return RedirectToPage("../Index");
        }
    }
}
