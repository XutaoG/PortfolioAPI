using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Update.DTO
{
	public class UpdateFeatureDto
	{
		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Description { get; set; } = null!;
	}
}