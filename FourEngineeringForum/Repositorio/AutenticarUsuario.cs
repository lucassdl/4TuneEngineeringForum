using FourEngineeringForum.Config;
using FourEngineeringForum.Models;
using FourEngineeringForum.Util;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FourEngineeringForum.Repositorio
{
    /// <summary>
    /// Classe responsável pela autenticação do usuário no sistema
    /// </summary>
    public class AutenticarUsuario
    {
        /// <summary>
        /// Método responsável por realizar a autenticação do usuário
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="senha">Senha</param>
        /// <returns>Retorna um usuário autenticado ou null</returns>
        public static Usuario AutenticaoUsuario(String login, String senha)
        {

            var senhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(senha, "MD5");

            try
            {
                using (ForumDbContext ctx = new ForumDbContext())
                {
                    var consultaUsuario = ctx.Usuarios.Where(x => x.Login == login && x.Senha == senhaCriptografada).FirstOrDefault();
                    return consultaUsuario;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método responsável por verificar a sessão do usuário
        /// </summary>
        /// <returns>Retorna valor booleano</returns>
        public static Boolean VerificarSessaoUsuario()
        {
            Usuario usuarioLogado = (Usuario)HttpContext.Current.Session["usuario"];

            if (usuarioLogado == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Método responsável por verificar a sessão do usuário administrador
        /// </summary>
        /// <returns>Retorna valor booleano</returns>
        public static Boolean VerificarSessaoUsuarioAdm()
        {
            Usuario usuarioLogado = (Usuario)HttpContext.Current.Session["usuario"];

            if (usuarioLogado != null && usuarioLogado.PerfilDoUsuario == Convert.ToChar(ListaDePerfisDoUsuario.Administrador))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método resposável por retornar o usuário logado
        /// </summary>
        /// <returns>Retorna o usuário logado</returns>
        public static Usuario RetornarUsuarioDaSessao()
        {
            Usuario usuarioLogado = (Usuario)HttpContext.Current.Session["usuario"];

            return usuarioLogado;
        }

        /// <summary>
        /// Método responsável por finalizar a sessão
        /// </summary>
        public static void FinalizarSessao()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}
