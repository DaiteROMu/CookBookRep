using System.Web.Mvc;

namespace CookBook.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {      
            return View();
        }
    }
}