using System;

namespace ElementLib
{
    public class Element
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int Z { get; set; }

        public override string ToString() => 
            $"{Z} {Name} ({Symbol})";
    }
}
