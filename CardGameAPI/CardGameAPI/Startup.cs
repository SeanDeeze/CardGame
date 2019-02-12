using CardGameAPI.Hubs;
using CardGameAPI.Models;
using CardGameAPI.Repositories;
using CardGameAPI.Repositories.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;

namespace CardGameAPI
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddOptions();
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
      });
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddDbContext<EFContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DbConnction")));
      services.AddSignalR(o =>
        {
          o.EnableDetailedErrors = true;
        });
      //services.AddSingleton<IGameEngine>(new GameEngine());
      services.AddSingleton(s => new GameEngine(new EFContext(new DbContextOptionsBuilder<EFContext>().UseSqlServer(Configuration.GetConnectionString("DbConnction")).Options)));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseCors("CorsPolicy");
      app.UseHttpsRedirection();

      app.Use(async (context, next) => // Add dynamic redirection for angular-based requests
      {
        await next();

        if (context.Response.StatusCode == 404 &&
            !Path.HasExtension(context.Request.Path.Value) &&
            !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });

      app.UseSignalR(routes =>
      {
        routes.MapHub<GameHub>("/api/gameHub");
      });

      app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
      app.UseMvc();
    }
  }
}
