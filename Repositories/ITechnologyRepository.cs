using Portfolio.API.Model;

namespace Portfolio.API.Repositories
{
	public interface ITechnologyRepository
	{
		Task<List<Technology>> GetAll();

		Task<Technology?> GetById(int id);

		Task<List<Technology>> GetAllByName(string name);

		// Task<Technology> Create(Technology technology);

		// Task<Technology?> UpdateById(int id, Technology technology);

		// Task<Technology?> DeleteById(int id);
	}
}