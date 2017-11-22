using EventsListBL.Container;
using EventsListData.Container;
using StructureMap;

namespace EventsListDependencies.Registries
{
    public class CommonRegistry:Registry
    {
        public CommonRegistry()
        {
            Scan(scan =>
            {
                scan.WithDefaultConventions();
                scan.Assembly(typeof(BusinessRegistry).Assembly);
                scan.Assembly(typeof(DataRegistry).Assembly);
            });
        }
    }
}
