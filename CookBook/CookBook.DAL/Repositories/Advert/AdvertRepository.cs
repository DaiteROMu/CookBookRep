using System.Collections.Generic;
using CookBook.DAL.AdvertServiceReference;
using CookBook.Common.Models;
using CookBook.DAL.Clients;

namespace CookBook.DAL.Repositories
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly IAdvertClient _advertClient;

        public AdvertRepository(IAdvertClient advertClient)
        {
            if (advertClient != null)
            {
                _advertClient = advertClient;
            }
            else
            {
                AdvertFactory factory = new AdvertFactory();
                _advertClient = factory.GetAdvertClient();
            }
        }

        private List<Advert> Convert(IEnumerable<AdvertType> advertTypesList)
        {
            if (advertTypesList == null)
            {
                return null;
            }

            List<Advert> adverts = new List<Advert>();

            foreach (AdvertType item in advertTypesList)
            {
                Advert advert = new Advert();
                advert.ImageId = item.ImageId;
                advert.ImageByteArray = item.ImageByteArray;
                adverts.Add(advert);
            }

            return adverts;
        }

        public IEnumerable<Advert> GetAdvertImages(string adverDirPath)
        {
            AdvertType[] advertTypesList = null;
            List<Advert> advertList = null;
            if (!string.IsNullOrEmpty(adverDirPath))
            {
                advertTypesList = _advertClient.GetAdvertImages(adverDirPath);
                advertList = Convert(advertTypesList);
            }

            return advertList;
        }

        public int[] GetRandomImageIds(string advertDirPath, int numberOfImages)
        {
            int[] ids = null;
            if (!string.IsNullOrEmpty(advertDirPath) && numberOfImages > 0)
            {
                ids = _advertClient.GetRandomImageIds(advertDirPath, numberOfImages);
            }

            return ids;
        }
    }
}
