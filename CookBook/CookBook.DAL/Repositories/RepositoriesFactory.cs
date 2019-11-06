using CookBook.Common.Log;
using CookBook.DAL.Clients;

namespace CookBook.DAL.Repositories
{
    public class RepositoriesFactory
    {
        private LoggerFactory _loggerFactory;
        private AdvertFactory _advertFactory;

        public RepositoriesFactory()
        {
            _loggerFactory = new LoggerFactory();
            _advertFactory = new AdvertFactory();
        }
        public IAdvertRepository GetAdvertRepository()
        {
            return new AdvertRepository(_advertFactory.GetAdvertClient());
        }

        public ICategoryRepository GetCategoryRepository()
        {
            return new CategoryRepository(_loggerFactory.GetLogger());
        }

        public IIngridientRepository GetIngridientRepository()
        {
            return new IngridientRepository(_loggerFactory.GetLogger());
        }

        public IRecipeRepository GetRecipeRepository()
        {
            return new RecipeRepository(_loggerFactory.GetLogger());
        }

        public IUserRepository GetUserRepository()
        {
            return new UserRepository(_loggerFactory.GetLogger());
        }
    }
}
