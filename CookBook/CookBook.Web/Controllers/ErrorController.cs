using System.Web.Mvc;
using CookBook.Web.AuthAttributes;

namespace CookBook.Web.Controllers
{
    [Guest]
    public class ErrorController : Controller
    {
        public ActionResult Unauthorized()
        {
            Response.StatusCode = 401;
            return View();
        }

        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }
        
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult InternalServerError()
        {
            Response.StatusCode = 500;
            return View();
        }
        
        public ActionResult BadGateway()
        {
            Response.StatusCode = 502;
            return View();
        }
    }
}