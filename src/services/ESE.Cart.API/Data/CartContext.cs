using ESE.Cart.API.Model;
using ESE.Core.Data;
using ESE.Core.DomainObjects;
using ESE.Core.Mediator;
using ESE.Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace ESE.Cart.API.Data
{
    public sealed class CartContext : DbContext
    {

        public CartContext(DbContextOptions<CartContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<CartClient> CartClient { get; set; }
        public DbSet<CartItem> CartItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.Entity<CartClient>()
                .HasIndex(c => c.ClientId)
                .HasDatabaseName("IDX_Client");

            modelBuilder.Entity<CartClient>()
                .HasMany(c => c.Items)
                .WithOne(i => i.CartClient)
                .HasForeignKey(c => c.CartId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}