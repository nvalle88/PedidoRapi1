using System.Net;
using System.Web.Mvc;

namespace MvcPedidos.Web
{
    public class AutorizationFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {


            }
            catch (CookieException)
            {

            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }


    }
}