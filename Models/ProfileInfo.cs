using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qualification.Models
{
    public class ProfileInfo
    {
        public IdentityUser User { get; set; }

        public long Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string MiddleName { get; set; }

    }
}
