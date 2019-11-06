using StructureMap.Configuration.DSL;
using CookBook.DAL.Repositories;
using CookBook.DAL.Clients;

namespace CookBook.DAL.Container
{
    public class DataRegistry: Registry
    {
        public DataRegistry()
        {
            For<ICategoryRepository>().Use<CategoryRepository>();
            For<IIngridientRepository>().Use<IngridientRepository>();
            For<IRecipeRepository>().Use<RecipeRepository>();
            For<IAdvertRepository>().Use<AdvertRepository>();
            For<IUserRepository>().Use<UserRepository>();
            For<IAdvertClient>().Use<AdvertClient>();            
        }
    }
}
