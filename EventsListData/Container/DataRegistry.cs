using EventsListData.Clients;
using EventsListData.Repositories;
using StructureMap;

namespace EventsListData.Container
{
   public class DataRegistry :Registry
    {
        public DataRegistry()
        {
            For<IDataRepository>().Use<DataRepository>();
            For<IClient>().Use<Client>();
        }
    }
}
