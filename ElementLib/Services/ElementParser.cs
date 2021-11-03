using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElementLib.Services
{
    public class ElementParser
    {
        public static Regex RadicalRegex = new(
            "(?<Symbol>[A-Z][a-z]*)(?<Number>\\d*)",
            RegexOptions.CultureInvariant | RegexOptions.Compiled);

        Dictionary<string, Element> _ElementTable = new();

        public void Init(IEnumerable<Element> elements)
        {
            _ElementTable = elements.ToDictionary(e => e.Symbol);
        }

        public Molecule Parse(string input)
        {
            var mol = new Molecule();

            var matches = RadicalRegex.Matches(input)
            .ToList();

            var counter = matches
                .Select(x => x.Length).Sum();

            if (counter != input.Length)
            {
                Console.WriteLine("Parse Error");
                throw new Exception("Parse Error");
            }

            foreach (var m in matches)
            {
                var element = _ElementTable[m.Groups["Symbol"].Value];
                var numStr = m.Groups["Number"].Value;
                
                int.TryParse(numStr, out var n);
                if (n == 0) n = 1;

                mol.Parts.Add(new Molecule.Part
                {
                    Element = element,
                    Number = n
                });
            }

            return mol;
        }
    }
}
