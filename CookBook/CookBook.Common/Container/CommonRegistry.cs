using CookBook.Common.Log;
using StructureMap.Configuration.DSL;

namespace CookBook.Common.Container
{
    public class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            For<ILogger>().Use<Logger>();
        }
    }
}