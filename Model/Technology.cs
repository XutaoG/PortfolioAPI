using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Portfolio.API.Model
{
	[Table(nameof(Technology))]
	public class Technology
	{
		[Key] // Specifies property as primary key
		public int Id { get; set; }

		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Description { get; set; } = null!;

		[ForeignKey(nameof(Project))] // Specifies property as foreign key
		public int ProjectId { get; set; }

		// Reference navigation to principal
		[JsonIgnore] // Prevent property from being serialized
		public Project Project { get; set; } = null!;
	}
}