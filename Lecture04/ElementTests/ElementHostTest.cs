using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ElementLib;
using ElementLib.Interfaces;
using ElementLib.Models;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace ElementTests
{
    public class ElementHostTest
    {
        readonly ITestOutputHelper _TestOutput;
        readonly ElementHostFactory _ApiFactory;
        readonly TestServer _ApiHost;

        public ElementHostTest(ITestOutputHelper testOutput)
        {
            _TestOutput = testOutput;

            _ApiFactory = new ElementHostFactory();

            _ApiHost = _ApiFactory.Server;
        }

        [Fact]
        public async Task GetElementsMongo()
        {
            try
            {
                await Task.Delay(100);

                var client = _ApiHost.CreateClient();

                var repo = _ApiHost.Services.GetService<IElementRepository>();
                Assert.NotNull(repo);

                //var dataPath = Path.GetDirectoryName(repo.GetType().Assembly.Location);
                //var filename = Path.Combine(dataPath, "Data", "TableOfElements.json");

                //var elements = ElementLoader.Load(filename);
                //await repo.InsertAsync(elements);

                var response = await client.GetAsync("api/element/elements");

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Bad Response: {response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var elementList = JsonConvert.DeserializeObject<List<Element>>(json);

                _TestOutput.WriteLine($"Got {elementList?.Count ?? 0} elements");

                Assert.True(elementList?.Count > 90);
            }
            catch (Exception ex)
            {
                _TestOutput.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                await _ApiFactory.ShutdownAsync();
            }
        }

        [Fact]
        public async Task GetElements()
        {
            try
            {
                var client = _ApiHost.CreateClient();

                var response = await client.GetAsync("api/element/elements");

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Bad Response: {response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var elementList = JsonConvert.DeserializeObject<List<Element>>(json);

                _TestOutput.WriteLine($"Got {elementList?.Count ?? 0} elements");

                Assert.True(elementList?.Count > 100);
            }
            finally
            {
                await _ApiFactory.ShutdownAsync();
            }
        }

        [Fact]
        public async Task Parse()
        {
            try
            {
                var client = _ApiHost.CreateClient();

                var response = await client.GetAsync($"api/element/parse/BrSO4");

                Assert.True(response.IsSuccessStatusCode);

                var json = await response.Content.ReadAsStringAsync();

                var mol = JsonConvert.DeserializeObject<Molecule>(json);

                AssertMolecule(mol, "Br", 1);
                AssertMolecule(mol, "S", 1);
                AssertMolecule(mol, "O", 4);
            }
            finally
            {
                await _ApiFactory.ShutdownAsync();
            }
        }

        void AssertMolecule(Molecule mol, string symbol, int n = 1)
        {
            Assert.Contains(mol.Parts, r => r.Element.Symbol == symbol && r.Number == n);
        }
    }
}
