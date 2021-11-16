using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Base;
using BaseLib;
using ElementBackend.Models;
using ElementLib;
using ElementLib.Enties;
using ElementLib.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ElementBackend.Services
{
    public class InitService : IHostedService
    {
        readonly ILogger<ElementService> _Logger;
        readonly IConfiguration _Configuration;
        readonly UserRepository _UserRepository;
        readonly IElementRepository _ElementRepository;

        public InitService(
            ILogger<ElementService> logger,
            IConfiguration configuration,
            UserRepository userRepository,
            IElementRepository elementRepository)
        {
            _Logger = logger;
            _Configuration = configuration;
            _UserRepository = userRepository;
            _ElementRepository = elementRepository;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            InitUsers();

            try
            {
                if (_ElementRepository is ElementLib.Infrastructure.MySql.ElementRepository mySqlRepo)
                    mySqlRepo.EnsureCreatedDataBase();

                IList<ElementEntity> elements = null;

                try
                {
                    elements = await _ElementRepository.ReadAsync();
                }
                catch (Exception e)
                {
                }
                if (elements.IsEmpty())
                {
                    var dataPath = Path.GetDirectoryName(this.GetType().Assembly.Location);
                    var filename = Path.Combine(dataPath, "Data", "TableOfElements.json");
                    var elementList = ElementLoader.Load(filename);

                    await _ElementRepository.InsertAsync(elementList);
                    _Logger.LogInformation("Element Table Loaded");
                }
            }
            catch(Exception ex)
            {
                _Logger.LogError("Failed of load Element Table");
            }
        }

        void InitUsers()
        {
            {
                var user = _Configuration["User"];
                var password = _Configuration["UserPassword"];

                if (_UserRepository.VerifyUser(user, password) != Role.User)
                    _UserRepository.AddUser(user, password, Role.User);
            }

            {
                var user = _Configuration["Admin"];
                var password = _Configuration["AdminPassword"];

                if (_UserRepository.VerifyUser(user, password) != Role.Admin)
                    _UserRepository.AddUser(user, password, Role.Admin);
            }

        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
