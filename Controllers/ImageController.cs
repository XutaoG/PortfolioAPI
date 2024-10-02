using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Add.DTO;
using Portfolio.API.Model;
using Portfolio.API.Repositories;
using Portfolio.API.Response.DTO;
using Portfolio.API.Update.DTO;

namespace Portfolio.API.Controllers
{
	[Route("api/image")]
	[ApiController]
	public class ImageController : ControllerBase
	{
		private readonly IProjectRepository projectRepository;
		private readonly IImageRepository imageRepository;
		private readonly IMapper mapper;

		public ImageController(
			IProjectRepository projectRepository, // Inject project repository
			IImageRepository imageRepository, // Inject image repository
			IMapper mapper // Inject AutoMapper
			)
		{
			this.projectRepository = projectRepository;
			this.imageRepository = imageRepository;
			this.mapper = mapper;
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetbyId([FromRoute] int id)
		{
			// Check for existence
			Image? image = await this.imageRepository.GetById(id);
			if (image == null)
			{
				// 404
				return NotFound();
			}

			// Map domain model to response DTO
			ImageResponseDto response = this.mapper.Map<ImageResponseDto>(image);

			return Ok(response);
		}

		[HttpPost]
		[Route("{projectId}")]
		public async Task<IActionResult> UploadImage(
			[FromRoute] int projectId,
			[FromForm] AddImageDto addImageDto
			)
		{
			// Validate image
			ValidateImageUpload(addImageDto);

			if (!ModelState.IsValid)
			{
				// 400
				return BadRequest(ModelState);
			}

			// Check for id existence
			Project? project = await this.projectRepository.GetById(projectId);
			if (project == null)
			{
				// 404
				return NotFound();
			}

			// Map add DTO to domain model
			Image image = this.mapper.Map<Image>(addImageDto);

			// Upload Image
			image = await this.imageRepository.Create(image, projectId);

			// Map domain model to response DTO
			ImageResponseDto response = this.mapper.Map<ImageResponseDto>(image);

			// 201
			return CreatedAtAction(nameof(GetbyId), new { id = response.Id }, response);
		}

		private void ValidateImageUpload(AddImageDto addImageDto)
		{
			// Check if image contains valid extension
			string[] allowedImageExts = [".jpg", ".png", ".jpeg"];
			if (allowedImageExts.Contains(Path.GetExtension(addImageDto.File.FileName).ToLower()) == false)
			{
				ModelState.AddModelError("file-format", "Unsupport file format");
			}
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] UpdateImageDto updateImageDto)
		{
			// Validate model
			if (!ModelState.IsValid)
			{
				// 400
				return BadRequest(ModelState);
			}

			// Map update DTO to domain model
			Image? image = this.mapper.Map<Image>(updateImageDto);

			// Update image
			image = await this.imageRepository.UpdateById(id, image);

			// Check for existence
			if (image == null)
			{
				// 404
				return NotFound();
			}

			// Map domain model to response DTO
			ImageResponseDto response = this.mapper.Map<ImageResponseDto>(image);

			// 200
			return Ok(response);
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteById([FromRoute] int id)
		{
			Image? image = await this.imageRepository.DeleteById(id);

			// Check for existence
			if (image == null)
			{
				// 404
				return NotFound();
			}

			// 204
			return NoContent();
		}
	}
}