using CookBook.DAL.Repositories;

namespace CookBook.Business.Providers
{
    public class ProvidersFactory
    {
        private RepositoriesFactory _repositoriesFactory;        

        public ProvidersFactory()
        {
            _repositoriesFactory = new RepositoriesFactory();
        }

        public AdvertProvider GetAdvertProvider()
        {
            return new AdvertProvider(_repositoriesFactory.GetAdvertRepository());
        }

        public CacheProvider GetCacheProvider()
        {
            return new CacheProvider();
        }

        public CategoryProvider GetCategoryProvider()
        {
            return new CategoryProvider(_repositoriesFactory.GetCategoryRepository(), GetCacheProvider());
        }

        public IngridientProvider GetIngridientProvider()
        {
            return new IngridientProvider(_repositoriesFactory.GetIngridientRepository());
        }

        public RecipeProvider GetRecipeProvider()
        {
            return new RecipeProvider(_repositoriesFactory.GetRecipeRepository());
        }

        public UserProvider GetUserProvider()
        {
            return new UserProvider(_repositoriesFactory.GetUserRepository());
        }
    }
}
