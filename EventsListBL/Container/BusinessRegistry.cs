using EventsListBL.Providers;
using EventsListBL.Providers.Interfaces;
using EventsListBL.Services;
using EventsListBL.Services.Interfaces;
using StructureMap;


namespace EventsListBL.Container
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            For<IEventProvider>().Use<EventProvider>();
            For<ICategoryProvider>().Use<CategoryProvider>();
            For<IAddressProvider>().Use<AddressProvider>();
            For<IUserProvider>().Use<UserProvider>();
            For<IEventOperation>().Use<EventOperation>();
            For<ICategoryOperation>().Use<CategoryOperation>();
            For<IAddressOperation>().Use<AddressOperation>();
            For<IUserOperation>().Use<UserOperation>();
            For<ILoginService>().Use<LoginService>();
        }
    }
}
