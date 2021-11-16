using System;
using System.Text;
using System.Threading.Tasks;
using Base;
using BaseLib;
using ElementBackend.Services;
using ElementLib;
using ElementLib.Enties;
using ElementLib.Infrastructure.Mongo;
using ElementLib.Infrastructure.MySql;
using ElementLib.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Serilog;
using ElementRepository = ElementLib.Infrastructure.Mongo.ElementRepository;

namespace ElementBackend
{
    public class Startup
    {
        ILogger<Startup> _Logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<ElementService>();
            services.AddSingleton<LoginService>();
            services.AddSingleton<UserRepository>();

            services.AddHostedService<InitService>();

            // InMem
            services.AddSingleton<ElementLib.Infrastructure.InMemory.ElementRepository>();

            //MongoDb
            var connectionStr = Configuration["Mongodb:ConnectionString"];

            if (connectionStr.IsNotEmpty())
            {
                var dbName = Configuration["Mongodb:Database"];
                var tableName = Configuration["Mongodb:TableName"];

                services.AddSingleton<IElementRepository, ElementRepository>();
                services.AddSingleton(sp => 
                    new MongoDb<ElementEntity>(new MongoClient(connectionStr), 
                        dbName, tableName));
            }
            else // MySQL
            {
                connectionStr = Configuration["MySql:ConnectionString"];

                if (connectionStr.IsNotEmpty())
                {
                    services.AddSingleton<IElementRepository, ElementLib.Infrastructure.MySql.ElementRepository>();

                    services.AddDbContextPool<ElementDataContext>(options =>
                        options.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr)));
                }
                else
                {
                    services.AddSingleton<IElementRepository, 
                        ElementLib.Infrastructure.InMemory.ElementRepository>();

                    Log.Information("Loading in Memory ElementRepository");
                }
            }

            AddSecurity(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ElementBackend", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                        new string[] {}
                    }
                });
            });
        }

        public void Configure(
            IApplicationBuilder app, 
            ILogger<Startup> logger,
            IServer server,
            IWebHostEnvironment env)
        {
            _Logger = logger;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ElementBackend v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            logger.LogInformation($"Application Started on url: " +
                                  $"{server.Features.Get<IServerAddressesFeature>()?.Addresses.ToCsv()}");
        }


        void AddSecurity(IServiceCollection services)
        {
            var key = Configuration["SECRET_KEY"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero // remove delay of token when expire
            };

            services
                .AddAuthentication(options =>
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            //services.AddAuthorization(options =>
            //{

            //});

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Admin", p =>
            //        p.RequireClaim("type", "Admin"));

            //    options.AddPolicy("User", p =>
            //    {
            //        p.RequireClaim("type", "User");
            //    });
            //});
        }

    }
}
