using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TvMazeScraper.API.Repository;
using TvMazeScraper.Domain.Interfaces;
using TvMazeScraper.Infrastructure;
using TvMazeScraper.Infrastructure.Interfaces;
using TvMazeScraper.Infrastructure.Repositories;

namespace TvMazeScraper.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository.Repository>();
            services.AddScoped<ITvMazeApiClient, TvMazeApiClient>();
            services.AddScoped<ITvShowDbRepository, TvShowDbRepository>();
            services.AddScoped<IBookmarkDbRepository, BookmarkDbRepository>();

            services.AddDbContext<TvMazeDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.UseInlineDefinitionsForEnums();
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Tv Maze Scraper", Version = "V1"});

                // Set the comments path for the Swagger JSON and UI.
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "TvMazeScraper.API.xml");
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TvMazeScraper.Api V1");
                });
        }
    }
}
