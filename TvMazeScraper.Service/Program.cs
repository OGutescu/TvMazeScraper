using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TvMazeScraper.Domain.Interfaces;
using TvMazeScraper.Infrastructure;
using TvMazeScraper.Infrastructure.Interfaces;
using TvMazeScraper.Infrastructure.Repositories;
using TvMazeScraper.Infrastructure.Services;

namespace TvMazeScraper.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main: setup dependencies...");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddAutoMapper(typeof(Program))
                .AddSingleton<IConfiguration>(configuration)

                .AddScoped<IApiScraperService, TvMazeApiScraperService>()
                .AddScoped<ITvMazeService, TvMazeService>()

                .AddScoped<IBookmarkDbRepository, BookmarkDbRepository>()
                .AddScoped<ITvShowDbRepository, TvShowDbRepository>()
                .AddDbContext<TvMazeDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))

                .AddScoped<ITvMazeApiClient, TvMazeApiClient>()
                .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                ;

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            StartBackgroundTasks(serviceProvider, logger);
        }

        private static void StartBackgroundTasks(ServiceProvider serviceProvider, ILogger<Program> logger)
        {
            Console.WriteLine("Main: Start background services...");

            var tvMazeScraper = serviceProvider.GetService<IApiScraperService>();

            var backgroundTasks = new[]
            {
                Task.Run(() => tvMazeScraper.Start()),
            };

            Task.WaitAll(backgroundTasks);
        }
    }
}
