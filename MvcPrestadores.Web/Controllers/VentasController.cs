using MvcPedidos.Services.Interface;
using MvcPrestadores.Entity.DTO;
using System;
using System.Collections.Generic;
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

            var bizTypes = await _serviceZoho.GetBizTypes(1);
            return View(bizTypes);
        }

        public async Task<ActionResult> GetSpecialities(int idBizType)
        {
            var idCustomerSession = System.Web.HttpContext.Current.Session["idCustomerSession"] as string;
            if (string.IsNullOrWhiteSpace(idCustomerSession))
                return RedirectToAction("Error", "Home", new { titleError = "Sección expirada", message = "La sección ha espirado" });

            System.Web.HttpContext.Current.Session["idCustomerSession"] = idCustomerSession;
            System.Web.HttpContext.Current.Session.Timeout = 2160;

            var bizTypes = await _serviceZoho.GetSpecialities(idBizType,1);
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

            var vendors = await _serviceZoho.GetVendorsBySpecialityAndCustomer(specialityName,idCustomerSession,1);
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

            var items = await _serviceZoho.GetItemsByVendor(vendorId,1);
            items = items.OrderBy(x => x.category).ThenByDescending(x => x.price).ThenBy(x => x.itemDescription).ToList();

            var itemsResult = items.GroupBy(x => x.category.Trim(),(key,x)=>new {category= key,id=Guid.NewGuid(),items=x.ToList() }).ToList();
            return Json(new
            {
                itemsResult
            },
            JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ProcessOrder(List<OrderDetails> details)
        {

            var idCustomerSession = System.Web.HttpContext.Current.Session["idCustomerSession"] as string;
            if (string.IsNullOrWhiteSpace(idCustomerSession))
                return RedirectToAction("Error", "Home", new { titleError = "Sección expirada", message = "La sección ha espirado" });

            System.Web.HttpContext.Current.Session["idCustomerSession"] = idCustomerSession;
            System.Web.HttpContext.Current.Session.Timeout = 2160;

            var order = new Order
            {
                idCustomer = idCustomerSession,
                details=details,
            };


            var idProcessed = await _serviceZoho.ProcessOrder(order,1);


            return Json(new
            {
                idProcessed,
                id=idCustomerSession,
            },
            JsonRequestBehavior.AllowGet);
        }
    }
}