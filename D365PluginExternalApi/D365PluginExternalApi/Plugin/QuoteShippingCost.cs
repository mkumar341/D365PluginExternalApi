using D365PluginExternalApi.Services;
using D365PluginExternalApi.Util;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365PluginExternalApi.Plugin
{
    public class QuoteShippingCost : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the tracing service
            ITracingService tracingService =
            (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.  
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // The InputParameters collection contains all the data passed in the message request.  
            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.  
                Entity entity = (Entity)context.InputParameters["Target"];

                // Obtain the organization service reference which you will need for  
                // web service calls.  
                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                try
                {
                    // Plug-in business logic goes here.  

                    if (context.Depth <= 1)
                    {
                        // Obtain the target entity from the input parmameters.
                        if (entity.LogicalName.Equals("quote"))
                        {
                            string country = entity.GetAttributeValue<string>("shipto_country");

                            var costService = new ExtrenalServiceShippingCost(new HttpClientApi());
                            string cost = costService.GetShippingCost(country).Result;

                            entity.Attributes["new_shipment"] = Convert.ToDecimal(cost); 



                        }
                    }
                }
                catch (Exception ex)
                {
                    tracingService.Trace("FollowUpPlugin: {0}", ex.ToString());
                    throw;
                }
            }
        }

    }
}
