using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Qualification.Data;
using Qualification.Models;
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
        private readonly UserManager<IdentityUser> _userManager;

        public AuthorizedAccountController(
            ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
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

            var employerViewModels = employers
                .Select(x => new
                {
                    x,
                    Profile =_dbContext.ProfileInfos.FirstOrDefault(y => y.UserId == x.Id)
                })
                .Select(x => new EmployerViewModel() 
                { 
                    Email = x.x.Email,
                    PhoneNumber = x.x.PhoneNumber,
                    Name = x.Profile?.Name ?? "",
                    MiddleName = x.Profile?.MiddleName?? "",
                    Surname = x.Profile?.SurName ?? ""
                })
                .ToList();

            return View(employerViewModels);
        }

        

        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Employee()
        {
            var employeeRole = await _roleManager
                .FindByNameAsync("employee")
                ?? throw new Exception("Не удалось найти роль");

            var employeeIdsQuery = _dbContext.UserRoles
                .Where(x => x.RoleId == employeeRole.Id)
                .Select(x => x.UserId);

            var employies = _dbContext.Users
                .Where(x => employeeIdsQuery.Any(id => id == x.Id))
                .ToList();

            var employerViewModels = employies
                .Select(x => new
                {
                    x,
                    Profile = _dbContext.ProfileInfos.FirstOrDefault(y => y.UserId == x.Id)
                })
                .Select(x => new EmployerViewModel()
                {
                    Email = x.x.Email,
                    PhoneNumber = x.x.PhoneNumber,
                    Name = x.Profile?.Name ?? "",
                    MiddleName = x.Profile?.MiddleName ?? "",
                    Surname = x.Profile?.SurName ?? ""
                })
                .ToList();

            return View(employerViewModels);
        }
    }
}
