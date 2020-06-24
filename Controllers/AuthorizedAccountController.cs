using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Qualification.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qualification.Controllers
{
    [Authorize]
    public class AuthorizedAccountController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthorizedAccountController(
            ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        public IActionResult GetEmployer()
        {

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Employer()
        {
            var employerRole = await _roleManager
                .FindByNameAsync("employer")
                ?? throw new Exception("Не удалось найти роль");

            var employerIdsQuery = _dbContext.UserRoles
                .Where(x => x.RoleId == employerRole.Id)
                .Select(x => x.UserId);

            var employers = _dbContext.Users
                .Where(x => employerIdsQuery.Any(id => id == x.Id))
                .ToList();

            return View(employers);
        }
    }
}
