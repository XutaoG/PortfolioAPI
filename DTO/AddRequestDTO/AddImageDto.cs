using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Add.DTO
{
	public class AddImageDto
	{
		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public IFormFile File { get; set; } = null!;

		public string? Description { get; set; } = null!;
	}
}