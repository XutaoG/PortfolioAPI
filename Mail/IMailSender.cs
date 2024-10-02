using Portfolio.API.Model;

namespace Portfolio.API.Mail
{
	public interface IMailSender
	{
		Task<string?> SendMail(EmailBody message);
	}
}