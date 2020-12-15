using FourEngineeringForum.Models;
using System.Data.Entity;
using System.Web.Security;

namespace FourEngineeringForum.Config
{
    public class InitializerDb : CreateDatabaseIfNotExists<ForumDbContext>
    {
        protected override void Seed(ForumDbContext context)
        {
            context.Usuarios.Add(new Usuario()
            {
                Nome = "admin",
                Login = "admin@fourengineering.com.pt",
                Senha = FormsAuthentication.HashPasswordForStoringInConfigFile("admin123", "MD5"),
                PerfilDoUsuario = 0
            });
        }
    }
}
