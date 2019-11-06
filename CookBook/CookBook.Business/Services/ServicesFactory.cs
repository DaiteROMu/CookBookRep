using CookBook.Business.Providers;

namespace CookBook.Business.Services
{
    public class ServicesFactory
    {
        private ProvidersFactory _providersFactory;

        public ServicesFactory()
        {
            _providersFactory = new ProvidersFactory();
        }

        public IAuthService GetAuthService()
        {
            return new AuthService(_providersFactory.GetUserProvider());
        }
    }
}
