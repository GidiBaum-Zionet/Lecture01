using BaseLib;
using System.Collections.Generic;
using System.Linq;

namespace ElementLib.Models
{
    public class Molecule
    {
        public record Part(Element Element, int Number = 0)
        {
            public override string ToString() => $"{Element.Symbol}*{Number}";
        }

        public List<Part> Parts { get; set; } = new();

        public float CalcMass() => Parts.Sum(x => x.Element.Mass * x.Number);

        public override string ToString() => Parts.ToCsv();

    }
}
