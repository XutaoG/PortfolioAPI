using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Portfolio.API.Data;
using Portfolio.API.Model;

namespace Portfolio.API.Repositories
{
	public class CloudinaryImageRepository : IImageRepository
	{
		// private readonly string cloudinaryApiKey;
		// private readonly string cloudinaryApiSecret;
		// private readonly string cloudName;

		private readonly PortfolioDbContext dbContext;
		private readonly Cloudinary cloudinary;

		public CloudinaryImageRepository(
			PortfolioDbContext dbContext, // Inject DbContext
			IConfiguration config // Inject configuration
			)
		{
			this.dbContext = dbContext;

			// Read secrets from config
			string cloudinaryApiKey = (string)config.GetValue(typeof(string), "CloudinaryApiKey")!;
			string cloudinaryApiSecret = (string)config.GetValue(typeof(string), "CloudinaryApiSecret")!;
			string cloudName = (string)config.GetValue(typeof(string), "CloudinaryCloudName")!;

			this.cloudinary = new Cloudinary($"cloudinary://{cloudinaryApiKey}:{cloudinaryApiSecret}@{cloudName}");
		}

		public async Task<Image?> GetById(int id)
		{
			// Get image
			Image? image = await this.dbContext.Images.Where(i => i.Id == id).FirstOrDefaultAsync();

			// Check for existence
			if (image == null)
			{
				return null;
			}


			// Set get parameters
			GetResourceParams getResourceParams = new GetResourceParams(image.Path);

			// get actual image URL
			string url = this.cloudinary.Api.UrlImgUp.Transform(new Transformation()
			.Width(1200).Chain()
			.Quality("auto").Chain()
			.FetchFormat("auto")
			).BuildUrl(image.Path);

			// Set image path
			image.Path = url;

			return image;
		}

		public async Task<Image> Create(Image image, int projectId)
		{
			// Set upload parameters
			ImageUploadParams uploadParams = new ImageUploadParams()
			{
				File = new FileDescription(image.Name, image.File.OpenReadStream()),
			};

			// Upload to Cloudinary
			UploadResult result = await this.cloudinary.UploadAsync(uploadParams);

			// Set image path
			image.Path = (string)result.JsonObj["public_id"]!;
			// Set foreign ID
			image.ProjectId = projectId;

			// Add image
			await this.dbContext.Images.AddAsync(image);
			// Persist change
			await this.dbContext.SaveChangesAsync();

			return image;
		}

		public async Task<Image?> UpdateById(int id, Image image)
		{
			// Check if id exists
			Image? foundImage = await this.dbContext.Images.Where(i => i.Id == id).FirstOrDefaultAsync();
			if (foundImage == null)
			{
				return null;
			}

			// Update image
			foundImage.Name = image.Name;
			foundImage.Description = image.Description;

			// Persist change to DB
			await this.dbContext.SaveChangesAsync();

			return foundImage;
		}

		public async Task<Image?> DeleteById(int id)
		{
			// Check if id exists
			Image? foundImage = await this.dbContext.Images.Where(t => t.Id == id).FirstOrDefaultAsync();
			if (foundImage == null)
			{
				return null;
			}

			// Delete iamge
			this.dbContext.Remove(foundImage);
			// Persist change to DB
			await this.dbContext.SaveChangesAsync();

			return foundImage;
		}
	}
}