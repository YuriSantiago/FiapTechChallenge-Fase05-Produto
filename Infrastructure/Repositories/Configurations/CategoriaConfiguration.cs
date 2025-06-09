using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repositories.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {

        public void Configure(EntityTypeBuilder<Categoria> builder)
        {

            builder.ToTable("Categoria");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(r => r.DataInclusao).HasColumnName("DataInclusao").HasColumnType("DATETIME").IsRequired();
            builder.Property(r => r.Descricao).HasColumnType("VARCHAR(100)").IsRequired();

        }

    }
}
