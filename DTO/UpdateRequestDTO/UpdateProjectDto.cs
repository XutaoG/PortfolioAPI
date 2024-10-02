using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Update.DTO
{
	public class UpdateProjectDto
	{
		[Required]
		public string Title { get; set; } = null!;

		[Required]
		public string Description { get; set; } = null!;

		[Required]
		public string ProjectType { get; set; } = null!;

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }

		[Required]
		public string Role { get; set; } = null!;

		public string? Link { get; set; }

		public string? Responsibility { get; set; }

		[Required]
		public List<UpdateTechnologyDto> Technologies { get; set; } = null!;

		[Required]
		public List<UpdateFeatureDto> Features { get; set; } = null!;

		// [Required]
		// public List<UpdateImageDto> Images { get; set; } = null!;
	}
}