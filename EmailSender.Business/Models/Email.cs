using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender.Business.Models
{
    public class Email
    {
        public List<String> To { get; set; }

        public List<String> Cc { get; set; }

        public List<String> Bcc { get; set; }

        public String Message { get; set; }

        public String Subject { get; set; }
    }
}
