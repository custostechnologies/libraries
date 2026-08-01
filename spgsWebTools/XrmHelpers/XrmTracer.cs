using Microsoft.Xrm.Sdk;

namespace spgsWebTools
{
    /// <summary>
    /// 
    /// </summary>
    public class XrmTracer
    {
        private readonly ITracingService _tracer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracer"></param>
        public XrmTracer(ITracingService tracer)
        {
            _tracer = tracer;
        }
    }
}