using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElementBackend;
using ElementLib.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongo2Go;
using MongoDB.Driver;

namespace ElementTests
{
    public class ElementHostFactory : WebApplicationFactory<Startup>
    {
        public IHost Host { get; private set; }
        public MongoDbRunner MongoDbRunner { get; }

        public ElementHostFactory()
        {
            MongoDbRunner = MongoDbRunner.Start();
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            IConfiguration configuration = null;

            builder.ConfigureAppConfiguration(config =>
            {
                configuration = new ConfigurationBuilder()
                    .AddJsonFile("testsettings.json")
                    .Build();

                config.AddConfiguration(configuration);
            });

            Host = builder
                .ConfigureServices(services =>
                {
                    // InMemory Database:
                    services.AddSingleton<IElementRepository,
                        ElementLib.Infrastructure.InMemory.ElementRepository>();

                    // MongoToGo Database:
                    //services.AddSingleton<IElementRepository,
                    //    ElementLib.Infrastructure.Mongo.ElementRepository>();

                    services.AddSingleton<IMongoClient>(_ =>
                        new MongoClient(MongoDbRunner.ConnectionString));
                })
                .Build();

            Host.StartAsync();

            return Host;

        }

        public async Task ShutdownAsync()
        {
            await Host.StopAsync();
            MongoDbRunner.Dispose();
        }
    }
}
