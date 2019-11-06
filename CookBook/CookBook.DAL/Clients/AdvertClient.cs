using System;
using System.ServiceModel;
using CookBook.DAL.AdvertServiceReference;
using CookBook.Common.Log;

namespace CookBook.DAL.Clients
{
    public class AdvertClient : IAdvertClient
    {
        private readonly ILogger _logger;

        public AdvertClient(ILogger logger)
        {
            if (logger != null)
            {
                _logger = logger;
            }
            else
            {
                LoggerFactory factory = new LoggerFactory();
                _logger = factory.GetLogger();
            }
        }

        public AdvertType[] GetAdvertImages(string directoryPath)
        {
            AdvertType[] advertTypes = null;
            if (!string.IsNullOrEmpty(directoryPath))
            {
                try
                {
                    using (AdvertContractClient client = new AdvertContractClient())
                    {
                        client.Open();
                        advertTypes = client.GetAdvertImages(directoryPath);
                        client.Close();
                    }
                    return advertTypes;
                }
                catch (FaultException ex)
                {
                    _logger.ErrorMessage(ex);
                    return advertTypes;
                }
                catch (Exception ex)
                {
                    _logger.ErrorMessage(ex);
                    return advertTypes;
                }
            }
            else
            {
                return advertTypes;
            }
        }

        public int[] GetRandomImageIds(string directoryPath, int numberOfImages)
        {
            int[] returnedIdsArray = null;
            if (!string.IsNullOrEmpty(directoryPath) && numberOfImages > 0)
            {
                try
                {
                    using (AdvertContractClient client = new AdvertContractClient())
                    {
                        client.Open();
                        returnedIdsArray = client.GetRandomImageIds(directoryPath, numberOfImages);
                        client.Close();
                    }
                    return returnedIdsArray;
                }
                catch (FaultException ex)
                {
                    _logger.ErrorMessage(ex);
                    return returnedIdsArray;
                }
                catch (Exception ex)
                {
                    _logger.ErrorMessage(ex);
                    return returnedIdsArray;
                }
            }
            else
            {
                return returnedIdsArray;
            }
        }
    }
}
