using Microsoft.AspNetCore.Authorization;
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

        public AuthorizedAccountController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult GetEmployer()
        {

            return View();
        }

        public IActionResult Employer()
        {
            var users = _dbContext.Users
                .ToList();

            return View(users);
        }
    }
}
