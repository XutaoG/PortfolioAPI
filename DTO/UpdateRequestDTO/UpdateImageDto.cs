using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Update.DTO
{
	public class UpdateImageDto
	{
		[Required]
		public string Name { get; set; } = null!;

		public string? Description { get; set; } = null!;
	}
}