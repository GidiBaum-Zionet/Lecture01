using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElementLib.Models;
using Newtonsoft.Json;
using ElementLib.Enties;

namespace ElementLib
{
    public static class ElementLoader
    {
        public static IEnumerable<ElementEntity> Load(string filename)
        {
            var json = File.ReadAllText(filename);

            var elementList = JsonConvert.DeserializeObject<List<Element>>(json);
            return elementList.Select(e => e.ToEntity());
        }
    }
}
