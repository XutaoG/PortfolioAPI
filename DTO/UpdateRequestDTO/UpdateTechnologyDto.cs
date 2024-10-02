using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Update.DTO
{
	public class UpdateTechnologyDto
	{
		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Description { get; set; } = null!;
	}
}