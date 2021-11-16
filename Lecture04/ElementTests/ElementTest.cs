using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElementLib;
using ElementLib.Models;
using Xunit;
using Xunit.Abstractions;

namespace ElementTests
{
    public class ElementTest
    {
        readonly ITestOutputHelper _TestOutput;

        public ElementTest(ITestOutputHelper testOutput)
        {
            _TestOutput = testOutput;
        }

        [Fact]
        public async Task ParseMolecule()
        {
            var repo = new ElementLib.Infrastructure.InMemory.ElementRepository();

            var dataPath = Path.GetDirectoryName(repo.GetType().Assembly.Location);
            var filename = Path.Combine(dataPath, "Data", "TableOfElements.json");

            var elements = ElementLoader.Load(filename);
            await repo.InsertAsync(elements);

            var elementService = new ElementService(repo);

            Molecule mol;

            // -----------------------------------------------------
            
            mol = await elementService.ParseAsync("H2O");

            AssertMolecule(mol, "H", 2);
            AssertMolecule(mol, "O", 1);

            // -----------------------------------------------------

            mol = await elementService.ParseAsync("NaCl");

            AssertMolecule(mol, "Na", 1);
            AssertMolecule(mol, "Cl", 1);

            // -----------------------------------------------------

            mol = await elementService.ParseAsync("C6O6H12");

            AssertMolecule(mol, "C", 6);
            AssertMolecule(mol, "O", 6);
            AssertMolecule(mol, "H", 12);

            // -----------------------------------------------------

            mol = await elementService.ParseAsync("Cs");

            AssertMolecule(mol, "Cs", 1);

        }

        void AssertMolecule(Molecule mol, string symbol, int n = 1)
        {
            Assert.Contains(mol.Parts, r => r.Element.Symbol == symbol && r.Number == n);
        }
    }
}
