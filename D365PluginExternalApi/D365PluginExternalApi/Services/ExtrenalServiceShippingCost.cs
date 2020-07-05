using D365PluginExternalApi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace D365PluginExternalApi.Services
{
    class ExtrenalServiceShippingCost : IExtrenalServiceShippingCost
    {
        private readonly IHttpClientApi _httpClientApi;

        public ExtrenalServiceShippingCost(IHttpClientApi httpClientApi)
        {
            _httpClientApi = httpClientApi;
        }

        public async Task<string> GetShippingCost(string country)
        {

            string cost = string.Empty;
            HttpResponseMessage shipment = await _httpClientApi.GetApiResponse($"shipment/{country}");
            if (shipment.IsSuccessStatusCode)
            {
                cost = await shipment.Content.ReadAsStringAsync();

            }
            return cost;
        }
    }
}
