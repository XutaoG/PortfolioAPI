using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Add.DTO;
using Portfolio.API.Mail;
using Portfolio.API.Model;

namespace Portfolio.API.Controllers
{
	[Route("api/mail")]
	[ApiController]
	public class MailController : ControllerBase
	{
		private readonly IMailSender sender;
		private readonly IMapper mapper;

		public MailController(
			IMailSender sender, // Inject IMailSender
			IMapper mapper // Inject AutoMapper
			)
		{
			this.sender = sender;
			this.mapper = mapper;
		}

		[HttpPost]
		[Route("send")]
		public async Task<IActionResult> Send([FromBody] SendEmailDto sendEmailDto)
		{
			// Map DTO to domain model
			EmailBody body = this.mapper.Map<EmailBody>(sendEmailDto);

			// Send mail
			string? response = await sender.SendMail(body);

			// If response is a string, an error occurred
			if (response != null)
			{
				// 500
				return StatusCode(500, response);
			}

			// 204
			return NoContent();
		}
	}
}