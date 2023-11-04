using ESE.Clients.API.Models;
using ESE.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESE.Clients.API.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                 .IsRequired()
                 .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Numero)
                .IsRequired()
                .HasMaxLength(Cpf.CpfMaxLength)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Address)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.AddressMaxLength})");
            });

            // 1 : 1 => Cliente => Endereço
            builder.HasOne(c => c.Address)
                   .WithOne(c => c.Client);

            builder.ToTable("Clients");
        }
    }
}
