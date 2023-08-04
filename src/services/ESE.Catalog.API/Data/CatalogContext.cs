using ESE.Catalog.API.Models;
using ESE.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace ESE.Catalog.API.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) 
            : base(options) { }

        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Esse foreach evita que se, caso haja um esquecimento de algum mapeamento,
            // ele faça com um varchar 100 automaticamente.
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)"); 

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }


        public async Task<bool> commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
