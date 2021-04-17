using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace MrBogomips.JiraCLI
{
    partial class Program
    {
      private static async Task<int> Main(string[] args)
        {
            try {
                var builder = CreateHostBuilder();
                return await builder.RunCommandLineApplicationAsync<JiraCliCommand>(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }
        
      private static IHostBuilder CreateHostBuilder() {
            // Configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "\\appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Logging
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            // Host
            var builder = new HostBuilder()
                .ConfigureServices ((hostCx, services) => {
                    services.AddLogging(config => {
                        config.ClearProviders();
                        config.AddProvider(new SerilogLoggerProvider(Log.Logger, false));
                        var minimumLevel = configuration.GetSection("Serilog:MinimumLevel")?.Value;
                        if (!string.IsNullOrEmpty(minimumLevel)) {
                            config.SetMinimumLevel(Enum.Parse<LogLevel>(minimumLevel));
                        }
                    });
                });

            return builder;
        }
    }
}