using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repositories.Configurations
{
    public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
    {

        public void Configure(EntityTypeBuilder<Contato> builder)
        {

            builder.ToTable("Contato");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(c => c.DataInclusao).HasColumnName("DataInclusao").HasColumnType("DATETIME").IsRequired();
            builder.Property(c => c.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(c => c.Telefone).HasColumnType("VARCHAR(11)").IsRequired();
            builder.Property(c => c.Email).HasColumnType("VARCHAR(150)").IsRequired();
            builder.Property(c => c.RegiaoId).HasColumnType("INT").IsRequired();

            builder.HasOne(c => c.Regiao)
                .WithMany(r => r.Contatos)
                .HasPrincipalKey(r => r.Id);

        }

    }
}
