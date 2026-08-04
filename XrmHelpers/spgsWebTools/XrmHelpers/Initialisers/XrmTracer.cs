using System;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.Shared.XrmHelpers.Initialisers
{
    public class XrmTracer
    {
        private readonly IServiceProvider _serviceProvider;
        public XrmTracer(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }
        public ITracingService Init()
        {
            var tracer = (ITracingService)_serviceProvider.GetService(typeof(ITracingService));
            tracer.Trace("Tracing Service Initialised, returning object to caller...");
            return tracer;
        }
    }
}