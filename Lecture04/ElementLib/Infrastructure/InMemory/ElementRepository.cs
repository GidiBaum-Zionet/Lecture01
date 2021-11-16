using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElementLib.Enties;
using ElementLib.Interfaces;

namespace ElementLib.Infrastructure.InMemory
{
    public class ElementRepository : IElementRepository
    {
        public Dictionary<string, ElementEntity> ElementMap { get; private set; }

        public Task<IList<ElementEntity>> ReadAsync()
        {
            if (ElementMap is null)
                ElementMap = new Dictionary<string, ElementEntity>();

            var list = ElementMap
                .Select(kv => kv.Value)
                .ToList();

            return Task.FromResult<IList<ElementEntity>>(list);
        }

        public Task InsertAsync(IEnumerable<ElementEntity> elements)
        {
            ElementMap = elements.ToDictionary(e => e.Symbol);

            return Task.CompletedTask;
        }

        public Task<ElementEntity> FindAsync(string symbol) => Task.FromResult(ElementMap[symbol]);
        
        public Task DeleteAllAsync()
        {
            ElementMap.Clear();
            return Task.CompletedTask;
        }
    }
}
