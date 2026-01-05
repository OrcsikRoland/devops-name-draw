
using Microsoft.EntityFrameworkCore;
using NameDraw.Api.Data;
using NameDraw.Api.Repositories;
using NameDraw.Api.Services;
using System.Security.Cryptography;

namespace NameDraw.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<INameRepository, EfNameRepository>();
            builder.Services.AddScoped<INameService, NameService>();
            builder.Services.AddCors(o =>
            {
                o.AddPolicy("AllowFrontend", p =>
                    p.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod());
            });
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["db:conn"]);
            });

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(
                    int.Parse(builder.Configuration["settings:port"] ?? "6500")
                );
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
                
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowFrontend");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
