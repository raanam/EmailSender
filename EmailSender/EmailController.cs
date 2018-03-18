using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailSender.Business.Models;
using EmailSender.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmailSender
{
  [Route("api/[controller]")]
  public class EmailController : Controller
  {
    private IEmailService _emailSvc;

    public EmailController(IEmailService emailService)
    {
      this._emailSvc = emailService;
    }

    // POST api/<controller>
    [HttpPost]
    public void Post([FromBody]Email email)
    {
      this._emailSvc.SendEmail(email);
    }
  }
}
