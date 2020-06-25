using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qualification.Dto
{
    public class UserFilterDto
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string MiddleName { get; set; }

        public string Education { get; set; }

        public string HistoryOfWork { get; set; }
    }
}
