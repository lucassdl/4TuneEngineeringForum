using FourEngineeringForum.Models;
using System.Data.Entity.ModelConfiguration;

namespace FourEngineeringForum.Maps
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable(nameof(Usuario));

            HasKey(pk => pk.ID);

            Property(u => u.Nome)
                .HasColumnName("USU_Nome")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(u => u.Login)
                .HasColumnName("USU_Login")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(u => u.Senha)
                .HasColumnName("USU_Senha")
                .HasColumnType("varchar")
                .HasMaxLength(32)
                .IsRequired();

            Property(u => u.PerfilDoUsuario)
                .HasColumnName("USU_TipoDeUsuario")
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
