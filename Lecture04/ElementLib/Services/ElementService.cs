using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ElementLib.Enties;
using ElementLib.Interfaces;
using ElementLib.Models;

namespace ElementLib
{
    public class ElementService
    {
        readonly IElementRepository _Repository;

        public ElementService(IElementRepository repository)
        {
            _Repository = repository;
        }

        public static Regex RadicalRegex = new(
            "(?<Symbol>[A-Z][a-z]*)(?<Number>\\d*)",
            RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public async Task<Molecule> ParseAsync(string input)
        {
            var mol = new Molecule();

            var ms = RadicalRegex.Matches(input);
            var matches = ms.Cast<Match>().ToList();

            var counter = matches.Select(x => x.Length).Sum();

            if (counter != input.Length)
            {
                Console.WriteLine("Parse Error");
                throw new Exception("Parse Error");
            }

            foreach (var match in matches)
            {
                var r = await ToRadicalAsync(match);
                mol.Parts.Add(r);
            }

            return mol;
        }

        async Task<Molecule.Part> ToRadicalAsync(Match m)
        {
            try
            {
                var entity = await _Repository.FindAsync(m.Groups["Symbol"].Value);
                var element = entity.ToModel();

                var numStr = m.Groups["Number"].Value;

                return new Molecule.Part(element, int.Parse(string.IsNullOrEmpty(numStr) ? "1" : numStr));
            }
            catch
            {
                throw new Exception("Not in dictionary");
            }
        }
    }
}
