using System.Web.Mvc;

namespace MvcPedidos.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          return  RedirectToAction("Index","Ventas");
        }

       
    }
}