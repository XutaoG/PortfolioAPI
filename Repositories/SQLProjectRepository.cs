using Microsoft.EntityFrameworkCore;
using Portfolio.API.Data;
using Portfolio.API.Model;

namespace Portfolio.API.Repositories
{
	public class SQLProjectRepository : IProjectRepository
	{
		private readonly PortfolioDbContext dbContext;

		public SQLProjectRepository(PortfolioDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<List<Project>> GetAll()
		{
			// return await this.dbContext.Projects.ToListAsync();
			return await this.dbContext.Projects
				.Include("Technologies")
				.Include("Features")
				.Include("Images")
				.AsQueryable()
				.OrderByDescending(p => p.EndDate) // Order by end date (descending)
				.ToListAsync();
		}

		public async Task<Project?> GetById(int id)
		{
			return await this.dbContext.Projects
				.Where(t => t.Id == id)
				.Include("Technologies")
				.Include("Features")
				.Include("Images")
				.FirstOrDefaultAsync();
		}

		public async Task<Project> Create(Project project)
		{
			// Add project
			await this.dbContext.Projects.AddAsync(project);

			// Apparent EFCore will add all foreign fields for you

			// Persist change to DB
			await this.dbContext.SaveChangesAsync();

			return project;
		}

		public async Task<Project?> UpdateById(int id, Project project)
		{
			// Check if id exists
			Project? foundProject = await this.dbContext.Projects
				.Where(t => t.Id == id)
				.Include("Technologies")
				.Include("Features")
				.Include("Images")
				.FirstOrDefaultAsync();

			if (foundProject == null)
			{
				return null;
			}

			// Update project
			foundProject.Title = project.Title;
			foundProject.Description = project.Description;
			foundProject.ProjectType = project.ProjectType;
			foundProject.StartDate = project.StartDate;
			foundProject.EndDate = project.EndDate;
			foundProject.Role = project.Role;
			foundProject.Link = project.Link;
			foundProject.Responsibility = project.Responsibility;
			foundProject.Technologies = project.Technologies;
			foundProject.Features = project.Features;

			// Persist change to DB
			await this.dbContext.SaveChangesAsync();

			return foundProject;
		}

		public async Task<Project?> DeleteById(int id)
		{
			// Check if id exists
			Project? foundProject = await this.dbContext.Projects.Where(t => t.Id == id).FirstOrDefaultAsync();
			if (foundProject == null)
			{
				return null;
			}

			// Delete project
			// Deletes all children as well
			this.dbContext.Remove(foundProject);

			// Persist change to DB
			await this.dbContext.SaveChangesAsync();

			return foundProject;
		}
	}
}