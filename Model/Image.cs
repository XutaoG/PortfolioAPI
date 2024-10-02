using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Portfolio.API.Model
{
	[Table(nameof(Image))]
	public class Image
	{
		[Key] // Specifies property as primary key
		public int Id { get; set; }

		[NotMapped] // Exclude from database mapping
		public IFormFile File { get; set; } = null!;

		public string Name { get; set; } = null!;

		public string Path { get; set; } = null!;

		public string? Description { get; set; }

		[ForeignKey(nameof(Project))] // Specifies property as foreign key
		public int ProjectId { get; set; }

		// Reference navigation to principal
		[JsonIgnore] // Prevent property from being serialized
		public Project Project { get; set; } = null!;
	}
}