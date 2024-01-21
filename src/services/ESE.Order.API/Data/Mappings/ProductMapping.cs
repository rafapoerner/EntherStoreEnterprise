using ESE.Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ESE.Catalog.API.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(x => x.Image)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.ToTable("Products");
        }
    }
}
