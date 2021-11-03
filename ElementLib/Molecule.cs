using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementLib
{
    public class Molecule
    {
        public class Part
        {
            public Element Element { get; set; }
            public int Number { get; set; }
        }

        public List<Part> Parts { get; set; } = new();
    }
}
