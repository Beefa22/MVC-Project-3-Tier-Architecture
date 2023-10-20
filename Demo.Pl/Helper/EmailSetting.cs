using Demo.DAL.Models;
using Demo.Pl.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;



namespace Demo.Pl.Helper
{
	public class EmailSetting:IEmailSettings
	{
        private readonly MailSettings _options;

        public EmailSetting(IOptions<MailSettings> options)
		{
           _options = options.Value;
        }
		public void SendEmail(Email email)
		{
			var mail = new MimeMessage
			{
				Sender = MailboxAddress.Parse(_options.Email),
				Subject = email.Subject
			};
			mail.To.Add(MailboxAddress.Parse(email.To));
			
			var builder = new BodyBuilder();
			builder.TextBody = email.Body;
			mail.Body = builder.ToMessageBody(); 
			mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

			using var smtp = new SmtpClient();
			smtp.Connect(_options.Host, _options.Port,SecureSocketOptions.StartTls);
			smtp.Authenticate(_options.Email, _options.Password);
			smtp.Send(mail); 
			smtp.Disconnect(true);
		}
	}
}
