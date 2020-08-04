using MvcPrestadores.Services.Interface;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcPrestadores.Web.Controllers
{
    public class VentasController : Controller
    {

        private readonly IServiceZoho _serviceZoho;

        public VentasController(IServiceZoho serviceZoho)
        {
            _serviceZoho = serviceZoho;
        }
        public async Task<ActionResult> Index()
        {
            var bizTypes = await _serviceZoho.GetBizTypes();
            return View(bizTypes);
        }

        public async Task<ActionResult> GetSpecialities(int idBizType)
        {

            var bizTypes = await _serviceZoho.GetSpecialities(idBizType);
            return Json(new
            {
                bizTypes
            },
            JsonRequestBehavior.AllowGet);
        }
    }
}