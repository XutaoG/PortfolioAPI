using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Add.DTO
{
	public class SendEmailDto
	{
		[Required]
		public string FirstName { get; set; } = null!;

		[Required]
		public string LastName { get; set; } = null!;

		[Required]
		public string Email { get; set; } = null!;

		[Required]
		public string Message { get; set; } = null!;
	}
}