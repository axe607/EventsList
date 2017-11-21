using EventsListBL.Providers;
using StructureMap;


namespace EventsListBL.Container
{
    public class BusinessRegistry:Registry
    {
        public BusinessRegistry()
        {
            For<IProvider>().Use<Provider>();
        }
    }
}
