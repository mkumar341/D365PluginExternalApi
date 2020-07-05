using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365PluginExternalApi.Services
{
    interface IExtrenalServiceShippingCost
    {
        Task<string> GetShippingCost(string country);
    }
}
