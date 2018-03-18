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
            foreach (var eachEmailProvider in _emailServiceProviders)
            {
                try
                {
                    eachEmailProvider.SendEmail(email);
                    break;
                }
                catch (Exception)
                {
                    // TODO: Log the failure and provider.
                }
            }
        }
    }
}
