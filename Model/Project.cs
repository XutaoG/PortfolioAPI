using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.API.Model
{
	[Table(nameof(Project))]
	public class Project
	{
		[Key] // Specifies property as primary key
		public int Id { get; set; }

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

		// Collection navigation containing dependents
		public List<Technology> Technologies { get; set; } = null!;

		// Collection navigation containing dependents
		public List<Feature> Features { get; set; } = null!;

		// Collection navigation containing dependents
		public List<Image> Images { get; set; } = null!;
	}
}