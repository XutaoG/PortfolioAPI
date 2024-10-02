namespace Portfolio.API.Response.DTO
{
	public class ImageResponseDto
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string Path { get; set; } = null!;

		public string? Description { get; set; }
	}
}