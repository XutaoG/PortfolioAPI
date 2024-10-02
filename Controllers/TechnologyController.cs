using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Add.DTO;
using Portfolio.API.Model;
using Portfolio.API.Repositories;
using Portfolio.API.Response.DTO;

namespace Portfolio.API.Controllers
{
	[Route("api/technology")]
	[ApiController]
	public class TechnologyController : ControllerBase
	{
		private readonly ITechnologyRepository technologyRepository;
		private readonly IMapper mapper;

		public TechnologyController(
			ITechnologyRepository technologyRepository,  // Inject Repository
			IMapper mapper // Inject AutoMapper
			)
		{
			this.technologyRepository = technologyRepository;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? name)
		{
			List<Technology> technologies;

			// Check if query string exists
			if (name is string)
			{
				// Get all technology domain models filtered by name
				technologies = await this.technologyRepository.GetAllByName(name);
			}
			else
			{
				// Get all technology domain models
				technologies = await this.technologyRepository.GetAll();
			}


			// Map domain models to response DTO
			List<TechnologyResponseDto> response = this.mapper.Map<List<TechnologyResponseDto>>(technologies);

			// 200
			return Ok(response);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			// Get technology
			Technology? technology = await this.technologyRepository.GetById(id);

			// Check for existence
			if (technology == null)
			{
				// 404
				return NotFound();
			}

			// Map domain model to response DTO
			TechnologyResponseDto response = this.mapper.Map<TechnologyResponseDto>(technology);

			// 200
			return Ok(response);
		}

		/*
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddTechnologyDto addTechnologyDto)
		{
			// Validate model
			if (!ModelState.IsValid)
			{
				// 400
				return BadRequest(ModelState);
			}

			// Map DTO to domain model
			Technology technology = this.mapper.Map<Technology>(addTechnologyDto);

			// Create Technology
			technology = await this.technologyRepository.Create(technology);

			// Map domain model into response DTO
			TechnologyResponseDto response = this.mapper.Map<TechnologyResponseDto>(technology);

			// 201
			return Created(new Uri(nameof(GetById)), response);
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteById([FromRoute] int id)
		{

			Technology? technology = await this.technologyRepository.DeleteById(id);

			// Check for existence
			if (technology == null)
			{
				// 404
				return NotFound();
			}

			// 204
			return NoContent();
		}
		*/
	}
}