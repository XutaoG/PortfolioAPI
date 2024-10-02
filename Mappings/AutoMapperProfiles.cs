using AutoMapper;
using Portfolio.API.Add.DTO;
using Portfolio.API.Model;
using Portfolio.API.Response.DTO;
using Portfolio.API.Update.DTO;

namespace Portfolio.API.Mappings
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			// Technologies
			CreateMap<Technology, TechnologyResponseDto>();
			CreateMap<AddTechnologyDto, Technology>();
			CreateMap<UpdateTechnologyDto, Technology>();

			// Projects
			CreateMap<Project, ProjectResponseDto>();
			CreateMap<AddProjectDto, Project>();
			CreateMap<UpdateProjectDto, Project>();

			// Features
			CreateMap<Feature, FeatureResponseDto>();
			CreateMap<AddFeatureDto, Feature>();
			CreateMap<UpdateFeatureDto, Feature>();

			// Images
			CreateMap<Image, ImageResponseDto>();
			CreateMap<AddImageDto, Image>();
			CreateMap<UpdateImageDto, Image>();

			// Email
			CreateMap<SendEmailDto, EmailBody>();
		}
	}
}