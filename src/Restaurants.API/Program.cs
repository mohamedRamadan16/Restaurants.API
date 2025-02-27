using Microsoft.OpenApi.Models;
using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;

namespace Restaurants.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.AddPresentation();
                builder.Services.AddInfrastructure(builder.Configuration); // for sql server connection
                builder.Services.AddApplication();

                var app = builder.Build();

                /// seeding when start the application
                var scope = app.Services.CreateScope();
                var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
                seeder.Seed();

                app.UseMiddleware<ErrorHandlingMiddleware>();
                app.UseMiddleware<TimeLogginMiddleware>();
                app.UseSerilogRequestLogging();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.MapGroup("api/identity")
                    .WithTags("Identity")
                    .MapIdentityApi<User>();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal("Application startub failed!!!!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
