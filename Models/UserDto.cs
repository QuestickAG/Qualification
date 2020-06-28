using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Qualification.Models
{
    public class UserDto
    {
        //public static Expression<Func<ProfileInfo, UserDto>> ProjectionExpression =
        //    profileInfo => new UserDto
        //    {
        //        Email = profileInfo.User.Email,
        //        PhoneNumber = profileInfo.User.PhoneNumber,
        //        Name = profileInfo.Name ?? string.Empty,
        //        MiddleName = profileInfo.MiddleName ?? string.Empty,
        //        Surname = profileInfo.SurName ?? string.Empty,
        //        Education = profileInfo.Education ?? string.Empty,
        //        HistoryOfWork = profileInfo.HistoryOfWork ?? string.Empty,
        //    };

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string Education { get; set; }

        public string HistoryOfWork { get; set; }
    }
}
