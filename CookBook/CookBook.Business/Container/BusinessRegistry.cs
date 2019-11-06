using StructureMap.Configuration.DSL;
using CookBook.Business.Providers;
using CookBook.Business.Services;

namespace CookBook.Business.Container
{
    public class BusinessRegistry: Registry
    {
        public BusinessRegistry()
        {
            For<ICategoryProvider>().Use<CategoryProvider>();
            For<ICacheProvider>().Use<CacheProvider>();
            For<IIngridientProvider>().Use<IngridientProvider>();
            For<IRecipeProvider>().Use<RecipeProvider>();
            For<IAdvertProvider>().Use<AdvertProvider>();
            For<IUserProvider>().Use<UserProvider>();
            For<IAuthService>().Use<AuthService>();
        }
    }
}