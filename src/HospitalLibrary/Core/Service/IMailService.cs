using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IMailService
    {
        Task SendBlockedEmailAsync(string mail, string name);
    }
}
