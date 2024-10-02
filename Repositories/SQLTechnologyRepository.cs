
using Microsoft.EntityFrameworkCore;
using Portfolio.API.Data;
using Portfolio.API.Model;

namespace Portfolio.API.Repositories
{
	public class SQLTechnologyRepository : ITechnologyRepository
	{
		private readonly PortfolioDbContext dbContext;

		public SQLTechnologyRepository(PortfolioDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<List<Technology>> GetAll()
		{
			return await this.dbContext.Technologies.ToListAsync();
		}


		public async Task<Technology?> GetById(int id)
		{
			return await this.dbContext.Technologies.Where(t => t.Id == id).FirstOrDefaultAsync();
		}

		public async Task<List<Technology>> GetAllByName(string name)
		{
			return await this.dbContext.Technologies
				.AsQueryable()
				.Where(t => t.Name.Contains(name))
				.ToListAsync();
		}

		/*
		public async Task<Technology> Create(Technology technology)
		{
			// Add technology
			await this.dbContext.Technologies.AddAsync(technology);

			// Persist change to DB
			await this.dbContext.SaveChangesAsync();

			return technology;
		}

		public async Task<Technology?> UpdateById(int id, Technology technology)
		{
			// Check if id exists
			Technology? foundTechnology = await this.dbContext.Technologies.Where(t => t.Id == id).FirstOrDefaultAsync();
			if (foundTechnology == null)
			{
				return null;
			}

			// Update technology
			foundTechnology.Name = technology.Name;
			foundTechnology.Description = technology.Description;

			// Persist change to DB
			await this.dbContext.SaveChangesAsync();

			return foundTechnology;
		}

		public async Task<Technology?> DeleteById(int id)
		{
			// Check if id exists
			Technology? foundTechnology = await this.dbContext.Technologies.Where(t => t.Id == id).FirstOrDefaultAsync();
			if (foundTechnology == null)
			{
				return null;
			}

			// Delete technology
			this.dbContext.Remove(foundTechnology);

			// Persist change to DB
			await this.dbContext.SaveChangesAsync();

			return foundTechnology;
		}
		*/
	}
}