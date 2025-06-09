using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repositories.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {

        public void Configure(EntityTypeBuilder<Produto> builder)
        {

            builder.ToTable("Produto");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(c => c.DataInclusao).HasColumnName("DataInclusao").HasColumnType("DATETIME").IsRequired();
            builder.Property(c => c.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(r => r.Descricao).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(c => c.Preco).HasColumnType("DECIMAL(18,2)").IsRequired();
            builder.Property(c => c.Disponivel).HasColumnType("BIT").IsRequired();
            builder.Property(c => c.CategoriaId).HasColumnType("INT").IsRequired();

            builder.HasOne(c => c.Categoria)
                .WithMany(r => r.Produtos)
                .HasPrincipalKey(r => r.Id);

        }

    }
}
