using _301290835_Maria_Josue_3013473439_Project.Models;
using Microsoft.EntityFrameworkCore;
using VeterinariansAPI.Services;

namespace VeterinariansAPI
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

            builder.Services.AddDbContext<Comp306ProjectContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("")));

            builder.Services.AddScoped<IVeterinariansRepository, VeterinarianRepository>();
            builder.Services.AddScoped<IDoctorsRepository, DoctorRepository>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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