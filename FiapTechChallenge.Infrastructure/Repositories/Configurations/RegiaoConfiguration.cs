using FiapTechChallenge.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapTechChallenge.Infrastructure.Repositories.Configurations
{
    public class RegiaoConfiguration : IEntityTypeConfiguration<Regiao>
    {

        public void Configure(EntityTypeBuilder<Regiao> builder)
        {

            builder.ToTable("Regiao");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(r => r.DataInclusao).HasColumnName("DataInclusao").HasColumnType("DATETIME").IsRequired();
            builder.Property(r => r.DDD).HasColumnType("SMALLINT").IsRequired();
            builder.Property(r => r.Descricao).HasColumnType("VARCHAR(100)").IsRequired();

        }

    }
}
