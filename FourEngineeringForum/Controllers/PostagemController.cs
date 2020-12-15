using FourEngineeringForum.Config;
using FourEngineeringForum.Models;
using FourEngineeringForum.Repositorio;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FourEngineeringForum.Controllers
{
    [VerificaUsuarioLogado]
    public class PostagemController : Controller
    {
        private ForumDbContext db = new ForumDbContext();

        /// <summary>
        /// Método responsável por exibir uma lista de Postagens cadastradas
        /// </summary>
        /// <param name="idDoTopico">Identificador da Postagem</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(int? idDoTopico)
        {
            var nomeTopico = db.Topicos.Find(idDoTopico);
            var postagem = db.Postagens.Where(p => p.Topico.ID == idDoTopico).Include(p => p.Topico).Include(p => p.Usuario);
            postagem.Count();

            if (nomeTopico != null)
            {
                ViewBag.Nome = nomeTopico.Nome;
            }

            return View(postagem);
        }

        /// <summary>
        /// Método responsável por preencher um selectList com Tópicos e Usuários cadastrados
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Adicionar()
        {
            ViewBag.IdDoTopico = new SelectList(db.Topicos, "ID", "Nome");
            ViewBag.IdDoUsuario = new SelectList(db.Usuarios, "ID", "Nome");
            return View();
        }

        /// <summary>
        /// Método responsável por salvar uma nova Postagem
        /// </summary>
        /// <param name="postagem">Postagem</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "ID,IdDoTopico,IdDoUsuario,Mensagem,DataDePublicacao")] Postagem postagem)
        {
            if (ModelState.IsValid)
            {
                Usuario usuarioLogado = AutenticarUsuario.RetornarUsuarioDaSessao();

                postagem.IdDoUsuario = usuarioLogado.ID;
                postagem.DataDePublicacao = DateTime.Now;

                db.Postagens.Add(postagem);
                db.SaveChanges();

                return RedirectToAction("Index", new { idDoTopico = postagem.IdDoTopico });
            }

            ViewBag.IdDoTopico = new SelectList(db.Topicos, "idDoTopico", "nome", postagem.IdDoTopico);
            ViewBag.IdDoUsuario = new SelectList(db.Usuarios, "idDoUsuario", "Nome", postagem.IdDoUsuario);
            return View(postagem);
        }

        /// <summary>
        /// Método responsável por exibir na tela, dados do post que serão editados
        /// </summary>
        /// <param name="idDoPost">ID</param>
        /// <returns>Exibe dados do post ou redireciona para a página de login</returns>>
        public ActionResult Editar(int? idDoPost)
        {
            if (AutenticarUsuario.VerificarSessaoUsuario())
            {
                if (idDoPost == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Postagem post = db.Postagens.Find(idDoPost);
                if (post == null)
                {
                    return HttpNotFound();
                }
                return View(post);
            }
            return Redirect("/Login");
        }

        /// <summary>
        /// Método responsável por editar os dados do post e gravar no banco de dados
        /// </summary>
        /// <param name="post">Usuário</param>
        /// <returns>Exibe dados do post ou redireciona para a página de login</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Postagem post)
        {
            if (AutenticarUsuario.VerificarSessaoUsuario())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(post).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { idDoTopico = post.IdDoTopico });
                }
                return View(post);
            }
            return Redirect("/Login");
        }

        /// <summary>
        /// Método responsável por fazer a chamada do Garbage Colector e limpar objetos não utilizados da memória
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
