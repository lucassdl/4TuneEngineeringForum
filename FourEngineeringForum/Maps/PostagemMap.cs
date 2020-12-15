using FourEngineeringForum.Models;
using System.Data.Entity.ModelConfiguration;

namespace FourEngineeringForum.Maps
{
    public class PostagemMap:EntityTypeConfiguration<Postagem>
    {
        public PostagemMap()
        {
            ToTable(nameof(Postagem));

            HasKey(pos => pos.ID);

            Property(pos => pos.Mensagem)
                .HasColumnName("POST_Mensagem")
                .HasColumnType("text")
                .HasMaxLength(100)
                .IsRequired();

            Property(pos => pos.DataDePublicacao)
                .HasColumnName("POST_DataDePublicacao")
                .HasColumnType("datetime")
                .IsRequired();

            HasRequired(pos => pos.Usuario)
                .WithMany()
                .HasForeignKey(pos => pos.IdDoUsuario);

            HasRequired(pos => pos.Topico)
                .WithMany()
                .HasForeignKey(pos => pos.IdDoTopico);
        }
    }
}
