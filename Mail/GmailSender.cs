using System.Net;
using System.Net.Mail;
using Portfolio.API.Model;

namespace Portfolio.API.Mail
{
	public class GmailSender : IMailSender
	{
		private readonly IConfiguration config;

		public GmailSender(IConfiguration config)
		{
			this.config = config;
		}

		public async Task<string?> SendMail(EmailBody emailMessage)
		{
			// Credentials
			object? emailSenderAddressObj = this.config.GetValue(typeof(string), "emailSenderAddress");
			object? emailSenderAuthPasswordObj = this.config.GetValue(typeof(string), "emailSenderAuthPassword");

			string emailSenderAddress;
			string emailSenderAuthPassword;

			// Check if auth exists
			if (emailSenderAddressObj != null &&
				emailSenderAuthPasswordObj != null)
			{
				emailSenderAddress = (string)emailSenderAddressObj;
				emailSenderAuthPassword = (string)emailSenderAuthPasswordObj;
			}
			else
			{
				// Return a string, indicating the error
				return "Invalid GmailSender authentication email or password.";
			}

			MailMessage message = new MailMessage();

			// Configure mail
			message.From = new MailAddress(emailSenderAddress);
			message.Subject = $"Portfolio Message from {emailMessage.FirstName} {emailMessage.LastName}";
			message.To.Add(new MailAddress("taogeeee@gmail.com"));
			message.Body = $"<html><body><p>From: {emailMessage.Email}</p><p>Name: {emailMessage.FirstName} {emailMessage.LastName}</p><p>Message: {emailMessage.Message}</p></body></html>";
			message.IsBodyHtml = true;

			// Configure SMTP client
			SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
			{
				UseDefaultCredentials = false,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Credentials = new NetworkCredential(emailSenderAddress, emailSenderAuthPassword),
				EnableSsl = true,
			};

			// Send email
			try
			{
				await client.SendMailAsync(message);
			}
			catch
			{
				// return a string, indicating the error
				return "The SMTP server requires a secure connection or the client was not authenticated.";
			}

			// return null, indicating no error
			return null;
		}
	}
}