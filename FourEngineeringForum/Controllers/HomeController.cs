using FourEngineeringForum.Config;
using System.Web.Mvc;

namespace FourEngineeringForum.Controllers
{
    public class HomeController : Controller
    {

        ForumDbContext ctx = new ForumDbContext();

        public ActionResult Index()
        {
            ctx.Usuarios.Find(3);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}