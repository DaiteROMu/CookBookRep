using StructureMap.Configuration.DSL;
using CookBook.Business.Container;
using CookBook.Common.Container;
using CookBook.DAL.Container;

namespace CookBook.Dependencies
{
    public class GeneralRegistry: Registry
    {
        public GeneralRegistry()
        {
            Scan(s=> {
                s.Assembly(typeof(BusinessRegistry).Assembly);
                s.Assembly(typeof(CommonRegistry).Assembly);
                s.Assembly(typeof(DataRegistry).Assembly);                
                s.WithDefaultConventions();
            });
        }
    }
}
