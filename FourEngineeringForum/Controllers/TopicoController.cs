using FourEngineeringForum.Config;
using FourEngineeringForum.Models;
using FourEngineeringForum.Repositorio;
using System.Linq;
using System.Web.Mvc;

namespace FourEngineeringForum.Controllers
{
    [VerificaUsuarioLogado]
    public class TopicoController : Controller
    {
        private ForumDbContext db = new ForumDbContext();

        /// <summary>
        /// Método responsável por listar todos os Tópicos
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            return View(db.Topicos.ToList());
        }

        /// <summary>
        /// Método responsavel por exibir a tela de novo Tópico
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Adicionar()
        {
            return View();
        }

        /// <summary>
        /// Método responsável por adicionar um novo Tópico
        /// </summary>
        /// <param name="topico">Tópico</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "ID,Nome,Descricao,DataDeCadastro")] Topico topico)
        {
            if (ModelState.IsValid)
            {
                db.Topicos.Add(topico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topico);
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
