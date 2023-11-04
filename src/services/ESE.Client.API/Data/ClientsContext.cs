using ESE.Clients.API.Models;
using ESE.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace ESE.Clients.API.Data
{
    public sealed class ClientsContext : DbContext, IUnitOfWork
    {
        public ClientsContext(DbContextOptions<ClientsContext> options)
        : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Adresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
               

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientsContext).Assembly);
        }


        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
