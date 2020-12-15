using FourEngineeringForum.Models;
using System.Data.Entity.ModelConfiguration;

namespace FourEngineeringForum.Maps
{
    public class TopicoMap:EntityTypeConfiguration<Topico>
    {
        public TopicoMap()
        {
            ToTable(nameof(Topico));

            HasKey(usu => usu.ID);

            Property(usu => usu.Nome)
                .HasColumnName("TOPI_Nome")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(usu => usu.Descricao)
                .HasColumnName("TOPI_Descricao")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(usu => usu.DataDeCadastro)
                .HasColumnName("TOPI_DataDeCadastro")
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
