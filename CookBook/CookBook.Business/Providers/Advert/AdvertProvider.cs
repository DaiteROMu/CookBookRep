using System.Collections.Generic;
using CookBook.Common.Models;
using CookBook.DAL.Repositories;

namespace CookBook.Business.Providers
{
    public class AdvertProvider : IAdvertProvider
    {
        private const string ADVERDIRPATH = @"E:\GitRepos\CookBookRep\AdvertImages";
        private const int NUMBERIMAGES = 3;        

        private readonly IAdvertRepository _advertRepository;

        public AdvertProvider(IAdvertRepository advertRepository)
        {
            if(advertRepository!=null)
            {
                _advertRepository = advertRepository;
            }
            else
            {
                RepositoriesFactory factory = new RepositoriesFactory();
                _advertRepository = factory.GetAdvertRepository();
            }        
        }

        public IEnumerable<Advert> GetAdvertImages()
        {            
            if(!string.IsNullOrEmpty(ADVERDIRPATH))
            {
                return _advertRepository.GetAdvertImages(ADVERDIRPATH);
            }
            else
            {
                return null;
            }
        }

        public int[] GetRandomImageIds()
        {
            if(!string.IsNullOrEmpty(ADVERDIRPATH) && NUMBERIMAGES > 1)
            {
                return _advertRepository.GetRandomImageIds(ADVERDIRPATH, NUMBERIMAGES);
            }
            else
            {
                return null;
            }
        }
    }
}
