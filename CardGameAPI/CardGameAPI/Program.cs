using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using NLog.Web;
using Microsoft.Extensions.Logging;

namespace CardGameAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // NLog: setup the logger first to catch all errors
      var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
      try
      {
        logger.Debug("init main");
        CreateWebHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        //NLog: catch setup errors
        logger.Error(ex, "Stopped program because of exception");
        throw;
      }
      finally
      {
        NLog.LogManager.Shutdown();
      }
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(logging =>
            {
              logging.ClearProviders();
              logging.SetMinimumLevel(LogLevel.Trace);
            })
            .UseNLog();  // NLog: setup NLog for Dependency injection
  }
}
