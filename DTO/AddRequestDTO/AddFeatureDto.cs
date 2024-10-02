using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Add.DTO
{
	public class AddFeatureDto
	{
		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Description { get; set; } = null!;
	}
}