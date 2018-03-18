using System;
using System.Collections.Generic;
using System.Text;
using EmailSender.Business.Models;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace EmailSender.Business.Services
{
    public class MailGunEmailSender : IEmailServiceProviders
    {
        private const string PROVIDER_KEY = "";

        public void SendEmail(Email email)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
            new HttpBasicAuthenticator("api", PROVIDER_KEY);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox6e1142287fac4845847432fb5383092f.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox6e1142287fac4845847432fb5383092f.mailgun.org>");

            if (email.To != null && email.To.Count > 0)
            {
                request.AddParameter("to", BuildSenderList(email.To));
            }

            if (email.Cc != null && email.Cc.Count > 0)
            {
                request.AddParameter("cc", BuildSenderList(email.Cc));
            }

            if (email.Bcc != null && email.Bcc.Count > 0)
            {
                request.AddParameter("bcc", BuildSenderList(email.Bcc));
            }

            request.AddParameter("subject", email.Subject);
            request.AddParameter("text", email.Message);

            request.Method = Method.POST;
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        private string BuildSenderList(List<string> recipients)
        {
            if (recipients != null && recipients.Count > 0)
            {
                // Wrap each recipient email address inside <>
                var formatted = recipients.Select(eachRx => $"<{eachRx.Trim()}>").ToList();

                return String.Join(",", formatted);
            }
            return string.Empty;
        }
    }
}
