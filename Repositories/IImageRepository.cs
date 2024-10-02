using Portfolio.API.Model;
using Portfolio.API.Update.DTO;

namespace Portfolio.API.Repositories
{
	public interface IImageRepository
	{
		Task<Image?> GetById(int id);

		Task<Image> Create(Image image, int projectId);

		Task<Image?> UpdateById(int id, Image image);

		Task<Image?> DeleteById(int id);
	}
}