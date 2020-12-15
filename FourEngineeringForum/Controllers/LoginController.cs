using FourEngineeringForum.Config;
using FourEngineeringForum.Models;
using FourEngineeringForum.Repositorio;
using FourEngineeringForum.Util;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace FourEngineeringForum.Controllers
{
    /// <summary>
    /// Controlador responsável pela autenticação e cadastro de usuários
    /// </summary>
    public class LoginController : Controller
    {
        private Usuario autenticarUsuario;

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método responsável pela autenticação do usuário
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="senha">Senha</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Login(String login, String senha)
        {
            autenticarUsuario = AutenticarUsuario.AutenticaoUsuario(login, senha);

            if (autenticarUsuario != null)
            {
                Session["usuarioAutenticado"] = autenticarUsuario.ID;
                Session["perfilDoUsuario"] = autenticarUsuario.PerfilDoUsuario;

                Session.Add("usuario", autenticarUsuario);
                return Redirect("/Topico/Index");
            }
            else
            {
                ViewBag.ErroAutenticacao = "Usuário ou senha inválidos.";
                return View("Index");
            }
        }

        /// <summary>
        /// Método responsável por cadastrar o usuário
        /// </summary>
        /// <param name="usuario">Usuario</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult CadastrarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ForumDbContext ctx = new ForumDbContext();

                    var senhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(usuario.Senha, "MD5");

                    usuario.Senha = senhaCriptografada;
                    usuario.PerfilDoUsuario = Convert.ToInt16(ListaDePerfisDoUsuario.Usuario);

                    ctx.Usuarios.Add(usuario);
                    ctx.SaveChanges();

                    ViewBag.StatusCadastro = "Cadastro realizado com sucesso.";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return View("Index");
        }

        /// <summary>
        /// Método responsável por finalizar a sessão
        /// </summary>
        public ActionResult FinalizarSessao()
        {
            AutenticarUsuario.FinalizarSessao();
            return RedirectToAction("Index");
        }
    }
}
