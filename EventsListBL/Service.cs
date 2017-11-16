using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsListBL
{
    public class Service : ITestServiceProvider
    {
        public string GetMessage()
        {
            return "Provider message";
        }
    }
}
