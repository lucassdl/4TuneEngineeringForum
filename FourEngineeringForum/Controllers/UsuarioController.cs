using FourEngineeringForum.Config;
using FourEngineeringForum.Models;
using FourEngineeringForum.Repositorio;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FourEngineeringForum.Controllers
{
    public class UsuarioController : Controller
    {
        private ForumDbContext db = new ForumDbContext();

        public ActionResult Index()
        {
            if (AutenticarUsuario.VerificarSessaoUsuario() && AutenticarUsuario.VerificarSessaoUsuarioAdm())
            {
                return View(db.Usuarios.ToList());
            }
            return Redirect("/Login");
        }

        /// <summary>
        /// Método responsável por exibir detalhes do usuário
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Exibe dados do usuário ou redireciona para a página de login</returns>
        public ActionResult Detalhes(int? id)
        {
            if (AutenticarUsuario.VerificarSessaoUsuario() && AutenticarUsuario.VerificarSessaoUsuarioAdm())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = db.Usuarios.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            return Redirect("/Login");
        }

        /// <summary>
        /// Método responsável por exibir na tela, dados do usuário que serão editados
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Exibe dados do usuário ou redireciona para a página de login</returns>>
        public ActionResult Adicionar()
        {
            if (AutenticarUsuario.VerificarSessaoUsuario() && AutenticarUsuario.VerificarSessaoUsuarioAdm())
            {
                return View();
            }
            return Redirect("/Login");
        }

        /// <summary>
        /// Método responsável por salvar um novo usuário
        /// </summary>
        /// <param name="usuario">Usuário</param>
        /// <returns>Exibe dados do usuário ou redireciona para a página de login</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "ID,Nome,Login,Senha,PerfilDoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (AutenticarUsuario.VerificarSessaoUsuario() && AutenticarUsuario.VerificarSessaoUsuarioAdm())
                {
                        db.Usuarios.Add(usuario);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                }
                return View(usuario);
            }
            return Redirect("/Login");
        }

        /// <summary>
        /// Método responsável por exibir na tela, dados do usuário que serão editados
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Exibe dados do usuário ou redireciona para a página de login</returns>>
        public ActionResult Editar(int? id)
        {
            if (AutenticarUsuario.VerificarSessaoUsuario() && AutenticarUsuario.VerificarSessaoUsuarioAdm())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = db.Usuarios.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            return Redirect("/Login");
        }

        /// <summary>
        /// Método responsável por editar os dados e gravar no banco de dados
        /// </summary>
        /// <param name="usuario">Usuário</param>
        /// <returns>Exibe dados do usuário ou redireciona para a página de login</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "ID,Nome,Login,Senha,PerfilDoUsuario")] Usuario usuario)
        {
            if (AutenticarUsuario.VerificarSessaoUsuario() && AutenticarUsuario.VerificarSessaoUsuario())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            return Redirect("/Login");
        }

        /// <summary>
        /// Método responsável por exibir na tela os dados para exclusão
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Exibe dados do usuário ou redireciona para a página de login</returns>
        public ActionResult Excluir(int? id)
        {
            if (AutenticarUsuario.VerificarSessaoUsuario() && AutenticarUsuario.VerificarSessaoUsuarioAdm())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = db.Usuarios.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            return Redirect("/Login");
        }

        /// <summary>
        /// Método responsável por remover o usuário
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Exibe dados do usuário ou redireciona para a página de login</returns>
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExclusao(int id)
        {
            if (AutenticarUsuario.VerificarSessaoUsuario() && AutenticarUsuario.VerificarSessaoUsuarioAdm())
            {
                Usuario usuario = db.Usuarios.Find(id);
                db.Usuarios.Remove(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Redirect("/Login");
        }
    }
}
