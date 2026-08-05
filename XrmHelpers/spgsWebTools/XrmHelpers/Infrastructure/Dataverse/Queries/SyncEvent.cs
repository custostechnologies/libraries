using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
using System.IdentityModel.Protocols.WSTrust;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    internal class AssignmentSyncEventService
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private readonly AttributeCollection _attributes;
        private readonly bool _syncAll;
        public AssignmentSyncEventService(
            IOrganizationService service, 
            ITracingService tracer, 
            string targetEntityLogicalName,
            string sourceEntityLogicalName,
            string syncEntityLogicalName,
            AttributeCollection attributes, 
            bool syncAll
        )
        {
            _service = service;
            _tracer = tracer;
            _attributes = attributes;
            _syncAll = syncAll;
        }
        public bool Create(string synMirrorId)
        {
            try
            {
                var syncEvent = new Entity(CREATE_ENTITY_LOGICAL_NAME);

                syncEvent["pgrowth_sourceentity"] = SOURCE_ENTITY_LOGICAL_NAME;
                syncEvent["pgrowth_targetentity"] = TARGET_ENTITY_LOGICAL_NAME;
                syncEvent["pgrowth_message"] = "update";
                syncEvent["statecode"] = new OptionSetValue((int)Status.Active);
                syncEvent["statuscode"] = new OptionSetValue((int)SyncEventStatusReason.Pending);
                syncEvent["pgrowth_sourcerecordid"] = synMirrorId;
                syncEvent["pgrowth_updatedattributes"] = string.Join(
                    ",",
                    _attributes.Keys.Select(key => key)
                );
                syncEvent["pgrowth_syncallattributes"] = _syncAll;

                _tracer.Trace("Creating sync event");

                _service.Create(syncEvent);

                _tracer.Trace("Sync event created");

                return true;
            }
            catch (Exception ex)
            {
                _tracer.Trace("Error constructing User Sync Event: {0}", ex);
                throw;
            }
        }
    }
}