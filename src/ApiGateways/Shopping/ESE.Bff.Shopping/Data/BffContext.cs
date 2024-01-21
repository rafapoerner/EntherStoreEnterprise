//using ESE.Catalog.API.Models;
//using ESE.Core.Data;
//using ESE.Core.Messages;
//using FluentValidation.Results;
//using Microsoft.EntityFrameworkCore;


//namespace ESE.Catalog.API.Data
//{
//    public class BffContext : DbContext, IUnitOfWork
//    {
//        public BffContext(DbContextOptions<BffContext> options) 
//            : base(options) { }

//        public DbSet<Product> Products { get; set; }


//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Ignore<ValidationResult>();
//            modelBuilder.Ignore<Event>();

//            // Esse foreach evita que se, caso haja um esquecimento de algum mapeamento,
//            // ele faça com um varchar 100 automaticamente.
//            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
//                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
//                property.SetColumnType("varchar(100)"); 

//            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BffContext).Assembly);
//        }


//        public async Task<bool> Commit()
//        {
//            return await base.SaveChangesAsync() > 0;
//        }
//    }
//}
