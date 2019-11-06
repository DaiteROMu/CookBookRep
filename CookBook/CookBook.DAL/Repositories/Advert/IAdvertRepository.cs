using System.Collections.Generic;
using CookBook.Common.Models;

namespace CookBook.DAL.Repositories
{
    public interface IAdvertRepository
    {
        IEnumerable<Advert> GetAdvertImages(string adverDirPath);

        int[] GetRandomImageIds(string advertDirPath, int numberOfImages);
    }
}
