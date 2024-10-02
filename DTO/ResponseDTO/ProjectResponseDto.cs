using Portfolio.API.Model;

namespace Portfolio.API.Response.DTO
{
	public class ProjectResponseDto
	{
		public int Id { get; set; }

		public string Title { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string ProjectType { get; set; } = null!;

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string Role { get; set; } = null!;

		public string? Link { get; set; }

		public string? Responsibility { get; set; }

		public ICollection<TechnologyResponseDto> Technologies { get; set; } = null!;

		public ICollection<FeatureResponseDto> Features { get; set; } = null!;

		public ICollection<ImageResponseDto> Images { get; set; } = null!;
	}
}