using CardGame.GameHub;
using CardGame.Models;
using CardGame.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
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
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
    builder.Host.UseNLog();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(CORS_POLICY,(CorsPolicyBuilder options) =>
        {
            options.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });

    string DBConnectionString = builder.Configuration.GetConnectionString("DbConnection");

    builder.Services.AddDbContext<EFContext>((DbContextOptionsBuilder options) =>
        options.UseSqlServer(DBConnectionString));

    builder.Services.AddSingleton<ICoordinator, Coordinator>((IServiceProvider s) =>
              new Coordinator(new EFContext(new DbContextOptionsBuilder<EFContext>().UseSqlServer(DBConnectionString).Options)));

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSignalR();

    builder.Services.AddSpaStaticFiles(configuration =>
    {
        configuration.RootPath = "./CardGameUI/dist";
    });

    WebApplication app = builder.Build();

    app.UseCors(CORS_POLICY);

    app.UseRouting();
    app.MapControllers();
    app.MapHub<GameHub>("/gameHub");

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseSpaStaticFiles();

        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = "./CardGameUI/dist";
        });
    }

    app.UseStaticFiles();

    app.Run();
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
    LogManager.Shutdown();
}