using MvcPrestadores.Services.Interface;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcPrestadores.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          return  RedirectToAction("Index","Ventas");
        }

       
    }
}