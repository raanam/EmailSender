using System;
using System.Collections.Generic;
using System.Text;
using EmailSender.Business.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace EmailSender.Business.Services
{
    internal class SendGridEmailRequest
    {
        public Personalization[] personalizations { get; set; }
        public From from { get; set; }
        public string subject { get; set; }
        public Content[] content { get; set; }
    }
    internal class From
    {
        public string email { get; set; }
    }

    internal class Personalization
    {
        public List<To> to { get; set; }
        public List<To> cc { get; set; }
        public List<To> bcc { get; set; }
    }

    internal class To
    {
        public string email { get; set; }
    }

    internal class Content
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class SendGridEmailSender : IEmailServiceProviders
    {
        private const string PROVIDER_KEY = "";

        public void SendEmail(Email email)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.sendgrid.com/v3/mail/send");

            RestRequest request = new RestRequest();
            request.AddParameter("Authorization", PROVIDER_KEY, ParameterType.HttpHeader);

            var body = new SendGridEmailRequest();
            body.personalizations = new Personalization[1] { new Personalization() };

            
            if (email.To != null && email.To.Count > 0)
            {
                body.personalizations[0].to = new List<To>();
                email.To.ForEach(eachRx =>
                {
                    body.personalizations[0].to.Add(new To() { email = eachRx.Trim() });
                });
            }
           
            if (email.Cc != null && email.Cc.Count > 0)
            {
                body.personalizations[0].cc = new List<To>();
                email.Cc.ForEach(eachRx =>
                {
                    body.personalizations[0].cc.Add(new To() { email = eachRx.Trim() });
                });
            }
            
            if (email.Bcc != null && email.Bcc.Count > 0)
            {
                body.personalizations[0].bcc = new List<To>();
                email.Bcc.ForEach(eachRx =>
                {
                    body.personalizations[0].bcc.Add(new To() { email = eachRx.Trim() });
                });
            }

            body.from = new From() { email = "ramanamgeo@gmail.com" };
            body.subject = email.Subject;
            body.content = new Content[1]
            {
                new Content()
                {
                    type = "text/html",
                    value = email.Message
                }
            };

            var serialisedBody = Newtonsoft.Json.JsonConvert.SerializeObject(body);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("content-type", "application/json");
            request.AddBody(body);
            request.Method = Method.POST;
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
    }
}
