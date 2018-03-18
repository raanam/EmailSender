using System;
using System.Collections.Generic;
using System.Text;
using EmailSender.Business.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSender.Business.Services
{
    public class EmailService : IEmailService
    {
        private IEnumerable<IEmailServiceProviders> _emailServiceProviders;

        public EmailService(IServiceProvider ioc)
        {
            _emailServiceProviders = ioc.GetServices<IEmailServiceProviders>();
        }

        public void SendEmail(Email email)
        {
            string errMsg = string.Empty;
            foreach (var eachEmailProvider in _emailServiceProviders)
            {
                try
                {
                    eachEmailProvider.SendEmail(email);
                    errMsg = string.Empty;
                    break;
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }

            if (string.IsNullOrWhiteSpace(errMsg) == false)
            {
                throw new Exception(errMsg);
            }
        }
    }
}
