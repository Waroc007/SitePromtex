using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromtexSite.Services
{
    public interface IEmailWorker
    {
        Task SendEmail(string text, string emailSentTo);
        Task SendEmail(string tel, string email, string comment, string emailSentTo);
    }
}
