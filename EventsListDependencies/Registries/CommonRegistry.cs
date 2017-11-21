using EventsListBL.Container;
using EventsListData.Container;
using StructureMap;

namespace EventsListDependencies.Registries
{
    class CommonRegistry:Registry
    {
        public CommonRegistry()
        {
            Scan(scan =>
            {
                scan.Assembly(typeof(DataRegistry).Assembly);
                scan.Assembly(typeof(BusinessRegistry).Assembly);
                scan.WithDefaultConventions();
            });
        }
    }
}
