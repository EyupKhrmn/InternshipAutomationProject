using System.Runtime.InteropServices;
using Org.BouncyCastle.Tls.Crypto;

namespace InternshipAutomation.Application.Mail;

public interface IEmailSender
{
   Task<string> SendEmailAsync(string receiverEmail,string receiverName, string subject, string content);
}