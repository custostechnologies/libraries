using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
using System.IdentityModel.Protocols.WSTrust;
using Enum = spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class SyncEvent
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        public SyncEvent(
            IOrganizationService service,
            ITracingService tracer
        )
        {
            _service = service;
            _tracer = tracer;
        }
        public Entity Build(
            string synMirrorId,
            string targetEntityLogicalName,
            string sourceEntityLogicalName,
            string syncEntityLogicalName,
            AttributeCollection attributes,
            bool syncAll
        )
        {
            try
            {
                var syncEvent = new Entity(syncEntityLogicalName);

                syncEvent["pgrowth_sourceentity"] = sourceEntityLogicalName;
                syncEvent["pgrowth_targetentity"] = targetEntityLogicalName;
                syncEvent["pgrowth_message"] = "update";
                syncEvent["statecode"] = new OptionSetValue((int)Enum.Status.Active);
                syncEvent["statuscode"] = new OptionSetValue((int)Enum.SyncEventStatusReason.Pending);
                syncEvent["pgrowth_sourcerecordid"] = synMirrorId;
                syncEvent["pgrowth_updatedattributes"] = string.Join(
                    ",",
                    attributes.Keys.Select(key => key)
                );
                syncEvent["pgrowth_syncallattributes"] = syncAll;

                var result = _service.Create(syncEvent);

                return syncEvent;
            }
            catch (Exception ex)
            {
                _tracer.Trace("Error constructing User Sync Event: {0}", ex);
                throw;
            }
        }
        public void Create(Entity syncEvent)
        {
            try 
            {
                _tracer.Trace("Creating sync event");
                _service.Create(syncEvent);
                _tracer.Trace("Sync event created");
            }  catch (Exception)
            {
                _tracer.Trace("Failed to create SyncEvent.");
                throw;
            }
        }

        public void UpdateStatus(Entity syncEvent, Enum.SyncEventStatusReason reason)
        {
            try
            {
                _tracer.Trace("Marking syncEvent status as complete"); 
                Enum.Status status = reason == Enum.SyncEventStatusReason.Complete
                    ? Enum.Status.Inactive
                    : Enum.Status.Active;
                syncEvent["statecode"] = new OptionSetValue((int)status);
                syncEvent["statuscode"] = new OptionSetValue((int)reason);
            } catch (Exception)
            {
                _tracer.Trace("Error encountered when setting syncEvent status to Inactive and statusReason to Complete.");
                throw;
            }
        }
    }
}