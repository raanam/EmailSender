﻿using System;
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
           
        }
    }
}