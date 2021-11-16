using System;
using ElementLib.Enties;
using Microsoft.EntityFrameworkCore;

namespace ElementLib.Infrastructure.MySql
{
    public class ElementDataContext : DbContext
    {
        public DbSet<ElementEntity> Elements { get; set; }

        public ElementDataContext(DbContextOptions<ElementDataContext> options)
            : base(options)
        {
            Console.WriteLine("ElementDataContext:CTOR");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ElementEntity>()
                .ToTable("Elements")
                .HasKey(e => e.Symbol);

            modelBuilder
                .Entity<ElementEntity>()
                .HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}
