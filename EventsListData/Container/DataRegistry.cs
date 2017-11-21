using EventsListData.Repositories;
using StructureMap;

namespace EventsListData.Container
{
   public class DataRegistry :Registry
    {
        public DataRegistry()
        {
            For<IDataProvider>().Use<Data>();
        }
    }
}
