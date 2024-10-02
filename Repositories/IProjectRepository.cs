using Portfolio.API.Model;

namespace Portfolio.API.Repositories
{
	public interface IProjectRepository
	{
		Task<List<Project>> GetAll();

		Task<Project?> GetById(int id);

		Task<Project> Create(Project project);

		Task<Project?> UpdateById(int id, Project project);

		Task<Project?> DeleteById(int id);
	}
}