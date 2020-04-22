using CrudPoc.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrudPoc.Repository
{
    public class CrudConfiguration : IEntityTypeConfiguration<AnnouncementWebMotors>
    {
        public void Configure(EntityTypeBuilder<AnnouncementWebMotors> builder)
        {
            builder.ToTable("tb_AnuncioWebmotors");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Make)
              .HasColumnName("Marca")
              .HasMaxLength(45);
            builder.Property(x => x.Model)
              .HasColumnName("Modelo")
              .HasMaxLength(45);
            builder.Property(x => x.Version)
              .HasColumnName("Versao")
              .HasMaxLength(45);
            builder.Property(x => x.Year)
              .HasColumnName("Ano");
            builder.Property(x => x.Mileage)
              .HasColumnName("quilometragem");
            builder.Property(x => x.Observation)
              .HasColumnName("observacao");
        }
    }
}
