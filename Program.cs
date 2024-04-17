
using Laptopy_APIs.Data;
using Laptopy_APIs.Repository;
using Laptopy_APIs.IRepository;
using Microsoft.EntityFrameworkCore;
using Laptopy_APIs.Models;
using Microsoft.AspNetCore.Identity;

namespace Laptopy_APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    // Ensure emails are unique
                    options.User.RequireUniqueEmail = true;
                }
            ).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<ILaptopRepository, LaptopRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IContactUsRepository, ContactUsRepository>();
            builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
