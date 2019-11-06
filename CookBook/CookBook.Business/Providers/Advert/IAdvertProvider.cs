using System.Collections.Generic;
using CookBook.Common.Models;

namespace CookBook.Business.Providers
{
    public interface IAdvertProvider
    {
        IEnumerable<Advert> GetAdvertImages();

        int[] GetRandomImageIds();
    }
}
