using CookBook.DAL.AdvertServiceReference;

namespace CookBook.DAL.Clients
{
    public interface IAdvertClient
    {
        AdvertType[] GetAdvertImages(string directoryPath);

        int[] GetRandomImageIds(string directoryPath, int numberOfImages);
    }
}
