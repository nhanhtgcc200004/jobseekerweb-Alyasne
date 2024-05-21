
using finalyearproject.SubSystem.Mailutils;
using System.Net;
using System.Net.Mail;

namespace EnterpriceWeb.Mailutils
{
    public class EmailSender : IEmailSender
    {
        public async Task<string> SenderEmailAsync(string email, string subject, string message)
        {
            string mail = "pluto.com2809@gmail.com";
            string password = "hzsexaebdbnrvhpf";
            var client = new SmtpClient()
            {
                EnableSsl = true,
                Port = 587,
                Host = "smtp.gmail.com",
                UseDefaultCredentials =false,
                
                Credentials = new System.Net.NetworkCredential(mail,password)

            };
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(mail);
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                await client.SendMailAsync(mailMessage);
                return "Email sent successfully!";
            }catch (Exception ex)
            {
                return $"Failed to send email: {ex.Message}";
            }
            
            
        }
        
    }
}
