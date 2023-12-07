using System.Net;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace InternshipAutomation.Application.Mail;

public class MailSender : IEmailSender
{
   private readonly SmtpSettings _smtpSettings;

   public MailSender(IOptions<SmtpSettings> smtpSettings)
   {
      _smtpSettings = smtpSettings.Value;
   }

   public async Task<string> SendEmailAsync(string receiverEmail, string receiverName, string subject, string content)
   {
      MimeMessage mimeMessage = new MimeMessage();
      MailboxAddress mailboxAddressFrom = new MailboxAddress("MAKÜ Staj", "eyupkhrmn45@gmail.com");
      MailboxAddress mailboxAddressTo = new MailboxAddress(receiverName, receiverEmail);

      mimeMessage.From.Add(mailboxAddressFrom);
      mimeMessage.To.Add(mailboxAddressTo);

      var bodyBuilder = new BodyBuilder();
      bodyBuilder.TextBody = content;
      mimeMessage.Body = bodyBuilder.ToMessageBody();

      mimeMessage.Subject = subject;

      SmtpClient client = new SmtpClient();
      client.Connect("smtp.gmail.com", 587, false);
      client.Authenticate("eyupkhrmn45@gmail.com","lnaw jiac oczw xhbj ");
      client.Send(mimeMessage);
      client.Disconnect(true);

      return "Mail Gönderildi";
   }
}