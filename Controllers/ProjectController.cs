using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Add.DTO;
using Portfolio.API.Model;
using Portfolio.API.Repositories;
using Portfolio.API.Response.DTO;
using Portfolio.API.Update.DTO;

namespace Portfolio.API.Controllers
{
	[Route("api/project")]
	[ApiController]
	public class ProjectController : ControllerBase
	{
		private readonly IProjectRepository projectRepository;
		private readonly IImageRepository imageRepository;
		private readonly IMapper mapper;

		public ProjectController(
			IProjectRepository projectRepository, // Inject project repository
			IImageRepository imageRepository, // Inject image repository
			IMapper Mapper // Inject AutoMapper
			)
		{
			this.projectRepository = projectRepository;
			this.imageRepository = imageRepository;
			this.mapper = Mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			// Get all projets
			List<Project> projects = await this.projectRepository.GetAll();

			// Map domain models to response DTO
			List<ProjectResponseDto> response = this.mapper.Map<List<ProjectResponseDto>>(projects);

			// 200
			return Ok(response);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			// Get project
			Project? project = await this.projectRepository.GetById(id);

			// Check for existenc
			if (project == null)
			{
				// 404
				return NotFound();
			}

			// Map domain model to response DTO
			ProjectResponseDto response = this.mapper.Map<ProjectResponseDto>(project);

			// 200
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddProjectDto addProject)
		{
			// Validate model
			if (!ModelState.IsValid)
			{
				// 400
				return BadRequest(ModelState);
			}

			// Map add DTO to domain model
			Project project = this.mapper.Map<Project>(addProject);

			// Create project
			project = await this.projectRepository.Create(project);

			// Map domain model to response DTO
			ProjectResponseDto response = this.mapper.Map<ProjectResponseDto>(project);

			// 201
			return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] UpdateProjectDto updateProjectDto)
		{
			// Validate model
			if (!ModelState.IsValid)
			{
				// 400
				return BadRequest(ModelState);
			}

			// Map update DTO to domain model
			Project? project = this.mapper.Map<Project>(updateProjectDto);

			// Update project
			project = await this.projectRepository.UpdateById(id, project);

			// Check for existence
			if (project == null)
			{
				// 404
				return NotFound();
			}

			// Map domain model to response DTO
			ProjectResponseDto response = this.mapper.Map<ProjectResponseDto>(project);

			// 200
			return Ok(response);
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteById([FromRoute] int id)
		{
			Project? project = await this.projectRepository.DeleteById(id);

			// Check for existence
			if (project == null)
			{
				// 404
				return NotFound();
			}

			// 204
			return NoContent();
		}
	}
}