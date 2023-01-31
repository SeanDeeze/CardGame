using CardGame.Models;
using CardGame.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;

Logger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    const string CORS_POLICY = "CorsPolicy";

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(CORS_POLICY,
            options =>
        {
            options
            .WithOrigins("http://localhost")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
    });

    builder.Services.AddDbContext<EFContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

    builder.Services.AddSingleton<IGameEngine, GameEngine>(s =>
              new GameEngine(new EFContext(new DbContextOptionsBuilder<EFContext>()
                                            .UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")).Options)));

    WebApplication app = builder.Build();

    app.UseDeveloperExceptionPage();
    app.UseCors(CORS_POLICY);
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
    app.UseStaticFiles();
    app.UseSpaStaticFiles();

    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "./CardGameUI/dist";

        if (app.Environment.IsDevelopment())
        {
            spa.Options.StartupTimeout = new TimeSpan(0, 0, 80);
            spa.UseAngularCliServer(npmScript: "start");
        }
    });
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}