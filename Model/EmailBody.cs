using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Model
{
	public class EmailBody
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