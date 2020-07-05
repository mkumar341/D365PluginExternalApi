using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace D365PluginExternalApi.Util
{
    public interface IHttpClientApi
    {
        Task<HttpResponseMessage> GetApiResponse(string queryPath);

    }
}
