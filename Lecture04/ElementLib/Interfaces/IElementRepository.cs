using System.Collections.Generic;
using System.Threading.Tasks;
using ElementLib.Enties;

namespace ElementLib.Interfaces
{
    public interface IElementRepository
    {
        Task<IList<ElementEntity>> ReadAsync();

        Task InsertAsync(IEnumerable<ElementEntity> elements);

        Task<ElementEntity> FindAsync(string symbol);

        Task DeleteAllAsync();
    }
}