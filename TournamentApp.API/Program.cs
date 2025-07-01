using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TournamentApp.API.Extensions;
using TournamentApp.Core.Repositories;
using TournamentApp.Data.Data;
using TournamentApp.Core.Repositories;
using TournamnetApp.Data.Repositories;
using TournamnetApp.Data.Data;
namespace TournamentApp.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TournamentAppAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentAppAPIContext") ?? throw new InvalidOperationException("Connection string 'TournamentAppAPIContext' not found.")));
            builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IUoW, UoW>();
            builder.Services.AddAutoMapper(typeof(TournamentMappings));
            var app = builder.Build();
            await app.SeedDataAsync();
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
