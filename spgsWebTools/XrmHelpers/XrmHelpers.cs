using System;
using Microsoft.Identity.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace spgsWebTools.Archive;

public class XrmHelpers
{
    

    // TO ADD: string array
    

    

    

    

    

    

    

    /// <summary>
    /// 
    /// </summary>
    public class ServiceInitialiser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static ITracingService InitTracingService(System.IServiceProvider provider, bool debug)
        {
            var tracingService = (ITracingService)provider.GetService(typeof(ITracingService));

            if (debug) tracingService.Trace("Tracing service initialised");

            return tracingService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="tracer"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static IOrganizationServiceFactory InitServiceFactory(System.IServiceProvider provider, ITracingService tracer, bool debug)
        {
            var orgServiceFactory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));

            if (debug) tracer.Trace("Organisation service factory initialised");

            return orgServiceFactory;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="factory"></param>
        /// <param name="tracer"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static IOrganizationService InitOrganisationService(IPluginExecutionContext context, IOrganizationServiceFactory factory, ITracingService tracer, bool debug)
        {
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);

            if (debug) tracer.Trace("Organisation service factory initialised");

            return service;
        }
    }

    public class OptionSetResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        

        /* helper to resolve Synergetic gender code to an OptionSet key value */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static int ResolveGenderKey(string code)
        {
            if (code == "M")
            {
                return 384810001;
            }
            else if (code == "F")
            {
                return 384810000;
            }
            else
            {
                return 384810004;
            }
        }
    }

    /*public static string CheckGroupMembership(string azureId, string groupId, string bearerToken)
    {
        
    }*/
}
