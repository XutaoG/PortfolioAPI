using Microsoft.EntityFrameworkCore;
using Portfolio.API.Model;

namespace Portfolio.API.Data
{
	public class PortfolioDbContext : DbContext
	{
		public DbSet<Project> Projects { get; set; }

		public DbSet<Technology> Technologies { get; set; }

		public DbSet<Feature> Features { get; set; }

		public DbSet<Image> Images { get; set; }

		public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
		{
		}
	}
}