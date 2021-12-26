using System;
using CardGame.Models;
using CardGame.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;

namespace CardGame
{
    public class Startup
    {
        private const string CORS_POLICY = "CorsPolicy";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            // Production Build Process replaces Origins localhost value with server IP
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder
                .WithOrigins("http://localhost")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            //If developing locally, avoid CORS issues by using this instead of above section:
            /*
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
            */

            services.AddSingleton<IGameEngine, GameEngine>(s =>
              new GameEngine(new EFContext(new DbContextOptionsBuilder<EFContext>()
                                            .UseSqlServer(Configuration.GetConnectionString("DbConnection")).Options)));

            services.AddDbContext<EFContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));


            services.AddControllers();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "./CardGameUI/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            GlobalDiagnosticsContext.Set("connectionString", Configuration.GetConnectionString("DefaultConnection"));
            app.UseDeveloperExceptionPage();
            app.UseCors(CORS_POLICY);
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(next => async context =>
            {
                if (context.Request.Path.Value != null 
                    && context.Request.Method == HttpMethods.Options 
                    && context.Request.Path.HasValue 
                    && context.Request.Path.Value.Contains("api/"))
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    return;
                }

                await next(context);
            });

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "./CardGameUI/dist";

                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = new TimeSpan(0, 0, 80);
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
