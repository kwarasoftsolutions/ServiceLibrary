using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ServicesLibrary
{
    public class MessagingService : IMessagingService
    {

        private SendGridClient _mailClient;
        private TwilioClient _textClient;


        public MessagingService()
        {

        }


        public MessagingService(SendGridClient mailClient, TwilioClient textClient)
        {
            _mailClient = mailClient ?? throw new ArgumentNullException();
            _textClient = textClient ?? throw new ArgumentNullException();
        }

        public async Task<bool> SendTextMessage(string from, string to, string subject, string textMessage)
        {
            var message = await MessageResource.CreateAsync(to: new PhoneNumber(to),from: new PhoneNumber(from),body: textMessage);
            if (message.Status == MessageResource.StatusEnum.Sent || message.Status == MessageResource.StatusEnum.Sending)               
            {
                return true;
            }
            return false;
        }

        public async Task<bool> SendMailMessage(string messengerName, string from, string to, string subject, string htmlMessage, string textMessage )
        {
            var sender = new EmailAddress(from, messengerName);
            var mailSubject = subject;
            var recipient = new EmailAddress(to, null);
            var plainTextContent = textMessage;
            var htmlContent = htmlMessage;
            var msg = MailHelper.CreateSingleEmail(sender, recipient, subject, plainTextContent, htmlContent);

            var response = await _mailClient.SendEmailAsync(msg);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }


    }
}
