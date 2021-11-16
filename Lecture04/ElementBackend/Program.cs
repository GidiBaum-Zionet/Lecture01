using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ElementBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddUserSecrets<Program>();
                })
                .UseSerilog(((context, configuration) =>
                {
                    var template = "{Level:u3} {Timestamp:HH:mm:ss} ({SourceContext}) {Message:lj}{NewLine}";

                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .WriteTo.Console(outputTemplate: template)
                        .WriteTo.Debug(outputTemplate: template);
                }))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
