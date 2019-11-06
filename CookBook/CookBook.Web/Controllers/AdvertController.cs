using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CookBook.Business.Providers;
using CookBook.Common.Models;
using CookBook.Web.AuthAttributes;

namespace CookBook.Web.Controllers
{
    [Guest]    
    public class AdvertController : Controller
    {
        private readonly IAdvertProvider _advertProvider;

        public AdvertController(IAdvertProvider advertProvider)
        {
            if (advertProvider != null)
            {
                _advertProvider = advertProvider;
            }
            else
            {
                ProvidersFactory factory = new ProvidersFactory();
                _advertProvider = factory.GetAdvertProvider();
            }
        }

        [ChildActionOnly]
        public ActionResult ShowAdverts()
        {
            int[] model = _advertProvider.GetRandomImageIds();
            return PartialView("_ShowAdverts", model);
        }

        [HttpGet]        
        public FileResult GetAdvertBanner(int advertId)
        {
            IEnumerable<Advert> adverts = _advertProvider.GetAdvertImages();
            byte[] arr = adverts.FirstOrDefault(a => a.ImageId == advertId).ImageByteArray;                   
            return File(arr, "image/png");
        }
    }
}