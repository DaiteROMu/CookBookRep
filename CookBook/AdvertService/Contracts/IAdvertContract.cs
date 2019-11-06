using System.Collections.Generic;
using System.ServiceModel;
using AdvertService.Models;

namespace AdvertService.Contracts
{
    [ServiceContract]
    public interface IAdvertContract
    {
        [OperationContract]
        List<AdvertType> GetAdvertImages(string directoryPath);

        [OperationContract]
        int[] GetRandomImageIds(string directoryPath, int numberOfImages);
    }
}
