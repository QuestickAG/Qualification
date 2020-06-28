using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Qualification.Data;
using Qualification.Dto;
using Qualification.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Qualification.Controllers
{
    public class SendEmailController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SendEmailController(
            ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> SendEmail()
        {
            var employeeRole = await _roleManager
                .FindByNameAsync("employee")
                ?? throw new Exception("Не удалось найти роль");

            var employeeIdsQuery = _dbContext.UserRoles
                .Where(x => x.RoleId == employeeRole.Id)
                .Select(x => x.UserId);

            var employees = _dbContext.ProfileInfos
                .Where(x => employeeIdsQuery.Contains(x.User.Id))
                .Select(profileInfo => new UserDto
                {
                    Email = profileInfo.User.Email,
                    PhoneNumber = profileInfo.User.PhoneNumber,
                    Name = profileInfo.Name ?? string.Empty,
                    MiddleName = profileInfo.MiddleName ?? string.Empty,
                    Surname = profileInfo.SurName ?? string.Empty,
                    Education = profileInfo.Education ?? string.Empty,
                    HistoryOfWork = profileInfo.HistoryOfWork ?? string.Empty,
                })
                .ToList();

            return View(employees);
        }

        [HttpPost]
        public ActionResult TrueSendEmail(SendEmailDto sendEmailDto)
        {
            var message = new MimeMessage();

            message.From.Add( new MailboxAddress("Саттаров Марсэль", "ooopartnercompany@gmail.com"));

            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", sendEmailDto.Email));

            message.Subject = sendEmailDto.Subject;

            message.Body = new TextPart("plain")
            {
                Text = sendEmailDto.Message
            };

            // configure and send email
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("ooopartnercompany@gmail.com", "pubgthebest");

                client.Send(message);
                client.Disconnect(true);
            }

            return RedirectToRoute(new { controller = "AuthorizedAccount", action = "Employee" });
        }
    }
}
