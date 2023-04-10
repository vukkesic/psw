using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class MailService : IMailService
    {
        public Task SendBlockedEmailAsync(string mail, string name)
        {
            throw new NotImplementedException();
        }
    }
}
