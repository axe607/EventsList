using EventsListBL.Providers;
using EventsListBL.Services;
using StructureMap;


namespace EventsListBL.Container
{
    public class BusinessRegistry:Registry
    {
        public BusinessRegistry()
        {
            For<IBusinessProvider>().Use<BusinessProvider>();
            For<IUserProvider>().Use<UserProvider>();
            For<ILoginService>().Use<LoginService>();
            For<IEventOperation>().Use<EventOperation>();
            For<IUserOperation>().Use<UserOperation>();
        }
    }
}
