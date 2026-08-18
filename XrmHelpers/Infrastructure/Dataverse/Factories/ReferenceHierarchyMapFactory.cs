using System;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class ReferenceHierarchyMapFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private CoreStaffMembers? _coreStaffQuery;
        private Systemusers? _systemuserQuery;
        public ReferenceHierarchyMapFactory(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
    }
}