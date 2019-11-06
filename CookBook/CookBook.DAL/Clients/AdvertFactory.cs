using CookBook.Common.Log;

namespace CookBook.DAL.Clients
{
    public class AdvertFactory
    {
        private LoggerFactory _loggerFactory;

        public AdvertFactory()
        {
            _loggerFactory = new LoggerFactory();
        }
        public IAdvertClient GetAdvertClient()
        {
            return new AdvertClient(_loggerFactory.GetLogger());
        }
    }
}
