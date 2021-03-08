using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Faker;
using Faker.Extensions;

using System.Net.Mail;
using MimeKit;

namespace api.Controllers
{
    // Just use action name as route
    [Route("[action]")]
    public class GenerateController : Controller
    {
        [HttpPost]
        public async Task EmailRandomNames(Range range, string email = "test@facke.com")
        {
            var message = new MailMessage("generator@generate.com",email);
            //message.From = (new MailAddress("","generator@generate.com"));
            //message.To.Add(new MailAddress(email));
            message.Subject = "Here are your random names";

            message.Body = new TextPart("plain")
            {
                Text = string.Join(Environment.NewLine, range.Of(Name.FullName))
            }.ToString();

            using (var mailClient = new SmtpClient())
            {
                mailClient.Host = "mail";
                mailClient.Port = 1025;
                mailClient.EnableSsl = false;
                //mailClient.Send(message);

                await mailClient.SendMailAsync(message);

            }

        }

        [HttpGet]
        public IEnumerable<string> Names(Range range)
            => range.Of(Name.FullName);

        [HttpGet]
        public IEnumerable<string> PhoneNumbers(Range range)
            => range.Of(Phone.Number);

        [HttpGet]
        public IEnumerable<int> Numbers(Range range)
            => range.Of(RandomNumber.Next);

        [HttpGet]
        public IEnumerable<string> Companies(Range range)
            => range.Of(Company.Name);

        [HttpGet]
        public IEnumerable<string> Paragraphs(Range range)
            => range.Of(() => Lorem.Paragraph(3));

        [HttpGet]
        public IEnumerable<string> CatchPhrases(Range range)
            => range.Of(Company.CatchPhrase);

        [HttpGet]
        public IEnumerable<string> Marketing(Range range)
            => range.Of(Company.BS);

        [HttpGet]
        public IEnumerable<string> Emails(Range range)
            => range.Of(Internet.Email);

        [HttpGet]
        public IEnumerable<string> Domains(Range range)
            => range.Of(Internet.DomainName);
    }
}
