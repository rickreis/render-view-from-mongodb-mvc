using System.Web.Mvc;

namespace ViewsFromMongoDB.Controllers
{
    public class HomeController : Controller
    {   
        public ActionResult Index()
        {
            ViewBag.Message = "Esta View está sendo lida do banco!";

            return View();
        }
    }
}