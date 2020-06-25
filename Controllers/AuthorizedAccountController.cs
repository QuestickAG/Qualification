using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Qualification.Data;
using Qualification.Dto;
using Qualification.Extensions;
using Qualification.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            var employerViewModels = employers
                .Select(x => new
                {
                    x,
                    Profile =_dbContext.ProfileInfos.FirstOrDefault(y => y.UserId == x.Id)
                })
                .Select(x => new UserDto() 
                { 
                    Email = x.x.Email,
                    PhoneNumber = x.x.PhoneNumber,
                    Name = x.Profile?.Name ?? "",
                    MiddleName = x.Profile?.MiddleName?? "",
                    Surname = x.Profile?.SurName ?? "",
                    Education = x.Profile?.Education ?? "",
                    HistoryOfWork = x.Profile?.HistoryOfWork ?? ""
                })
                .ToList();

            return View(employerViewModels);
        }

        

        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Employee(UserFilterDto filterParams)
        {
            var employeeRole = await _roleManager
                .FindByNameAsync("employee")
                ?? throw new Exception("Не удалось найти роль");

            var employeeIdsQuery = _dbContext.UserRoles
                .Where(x => x.RoleId == employeeRole.Id)
                .Select(x => x.UserId);

            var employees = _dbContext.ProfileInfos
                .Where(x => employeeIdsQuery.Contains(x.User.Id))
                .Select(x => new UserDto
                {
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber,
                    Name = x.Name ?? string.Empty,
                    MiddleName = x.MiddleName ?? string.Empty,
                    Surname = x.SurName ?? string.Empty,
                    Education = x.Education ?? string.Empty,
                    HistoryOfWork = x.HistoryOfWork ?? string.Empty,
                })
                .FilterEmployees(filterParams)
                .ToList();

            return View(employees);
        }
    }
}
