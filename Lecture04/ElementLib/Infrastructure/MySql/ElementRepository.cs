using System.Collections.Generic;
using System.Threading.Tasks;
using ElementLib.Enties;
using ElementLib.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ElementLib.Infrastructure.MySql
{
    public class ElementRepository : IElementRepository
    {
        readonly IServiceScopeFactory _ScopeFactory;

        public ElementRepository(IServiceScopeFactory scopeFactory)
        {
            _ScopeFactory = scopeFactory;
        }

        public async Task InsertAsync(IEnumerable<ElementEntity> elements)
        {
            using var scope = _ScopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ElementDataContext>();

            db.Elements.AddRange(elements);
            await db.SaveChangesAsync();
        }

        public async Task<ElementEntity> FindAsync(string symbol)
        {
            using var scope = _ScopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ElementDataContext>();

            return await db.Elements.FirstOrDefaultAsync(e => e.Symbol == symbol);
        }

        public async Task<IList<ElementEntity>> ReadAsync()
        {
            using var scope = _ScopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ElementDataContext>();

            return await db.Elements.ToListAsync();
        }

        public async Task DeleteAllAsync()
        {
            using var scope = _ScopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ElementDataContext>();

            db.Elements.RemoveRange(db.Elements);

            await db.SaveChangesAsync();
        }


        public void EnsureCreatedDataBase()
        {
            using var scope = _ScopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ElementDataContext>();

            db.Database.EnsureCreated();
        }
    }
}
