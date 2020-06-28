using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qualification.Dto
{
    public class SendEmailDto
    {
        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
