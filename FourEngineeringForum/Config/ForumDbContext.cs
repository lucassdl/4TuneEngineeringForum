using FourEngineeringForum.Models;
using System.Data.Entity;

namespace FourEngineeringForum.Config
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext() : base("ForumDbConn")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ForumDbContext>());
            Database.SetInitializer(new InitializerDb());
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Topico> Topicos { get; set; }
        public DbSet<Postagem> Postagens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Maps.UsuarioMap());
            modelBuilder.Configurations.Add(new Maps.TopicoMap());
            modelBuilder.Configurations.Add(new Maps.PostagemMap());
        }
    }
}
