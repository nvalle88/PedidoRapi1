using MvcPedidos.Services.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcPedidos.Web.Controllers
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
            var idCustomerSession = System.Web.HttpContext.Current.Session["idCustomerSession"] as string;
            if (string.IsNullOrWhiteSpace(idCustomerSession))
                return RedirectToAction("Error", "Home", new { titleError = "Sección expirada", message = "La sección ha espirado" });

            System.Web.HttpContext.Current.Session["idCustomerSession"] = idCustomerSession;
            System.Web.HttpContext.Current.Session.Timeout = 2160;

            var bizTypes = await _serviceZoho.GetBizTypes();
            return View(bizTypes);
        }

        public async Task<ActionResult> GetSpecialities(int idBizType)
        {
            var idCustomerSession = System.Web.HttpContext.Current.Session["idCustomerSession"] as string;
            if (string.IsNullOrWhiteSpace(idCustomerSession))
                return RedirectToAction("Error", "Home", new { titleError = "Sección expirada", message = "La sección ha espirado" });

            System.Web.HttpContext.Current.Session["idCustomerSession"] = idCustomerSession;
            System.Web.HttpContext.Current.Session.Timeout = 2160;

            var bizTypes = await _serviceZoho.GetSpecialities(idBizType);
            return Json(new
            {
                bizTypes
            },
            JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetVendorsBySpecialityAndCustomer(string specialityName)
        {
            var idCustomerSession = System.Web.HttpContext.Current.Session["idCustomerSession"] as string;
            if (string.IsNullOrWhiteSpace(idCustomerSession))
                return RedirectToAction("Error", "Home", new { titleError = "Sección expirada", message = "La sección ha espirado" });

            System.Web.HttpContext.Current.Session["idCustomerSession"] = idCustomerSession;
            System.Web.HttpContext.Current.Session.Timeout = 2160;

            var vendors = await _serviceZoho.GetVendorsBySpecialityAndCustomer(specialityName,idCustomerSession);
            return Json(new
            {
                vendors
            },
            JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> GetItemsByVendor(string vendorId)
        {
            var idCustomerSession = System.Web.HttpContext.Current.Session["idCustomerSession"] as string;
            if (string.IsNullOrWhiteSpace(idCustomerSession))
                return RedirectToAction("Error", "Home", new { titleError = "Sección expirada", message = "La sección ha espirado" });

            System.Web.HttpContext.Current.Session["idCustomerSession"] = idCustomerSession;
            System.Web.HttpContext.Current.Session.Timeout = 2160;

            var items = await _serviceZoho.GetItemsByVendor(vendorId);
            items= items.OrderBy(x => x.category).ThenByDescending(x => x.price).ThenBy(x => x.itemDescription).ToList();
            return Json(new
            {
                items
            },
            JsonRequestBehavior.AllowGet);
        }
    }
}