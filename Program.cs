using Microsoft.EntityFrameworkCore;
using Portfolio.API.Data;
using Portfolio.API.Mail;
using Portfolio.API.Mappings;
using Portfolio.API.Repositories;

namespace Portfolio
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add controllers to the container
			builder.Services.AddControllers();

			// Add services to the container
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Add DbContext to the container
			builder.Services.AddDbContext<PortfolioDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioDbConnectionString"));
			});

			// Add Repositories to the container
			builder.Services.AddScoped<ITechnologyRepository, SQLTechnologyRepository>();
			builder.Services.AddScoped<IProjectRepository, SQLProjectRepository>();
			builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();

			// Add Mail Sender to the container
			builder.Services.AddTransient<IMailSender, GmailSender>();

			// Prevent JSON cycles
			// builder.Services.AddControllers().AddJsonOptions(x =>
			// 	x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

			// Add AutoMapper to the container
			builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.MapControllers();
			app.Run();
		}
	}
}

