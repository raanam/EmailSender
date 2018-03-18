using EmailSender.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender.Business.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}
