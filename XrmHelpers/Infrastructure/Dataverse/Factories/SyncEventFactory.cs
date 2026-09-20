using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using Enum = spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class SyncEventFactory
    {
        private readonly IDataverseContext _context;
        private readonly IOrganizationService _service;
        private readonly DataverseLogger _logger;
        private readonly string _correlationId;
        public SyncEventFactory(
            IDataverseContext context,
            string correlationId
        )
        {
            _context = context;
            _correlationId = correlationId;

            _logger = new DataverseLogger(
                _context.Tracer,
                typeof(SyncEventFactory).Name
            );
            _service = _context.Service;
        }
        public Entity Build(
            string synMirrorId,
            string targetEntityLogicalName,
            string sourceEntityLogicalName,
            string syncEntityLogicalName
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
                syncEvent["pgrowth_correlationid"] = _correlationId;
                //syncEvent["pgrowth_exitearly"] = exitEarly;
                /*syncEvent["pgrowth_updatedattributes"] = string.Join(
                    ",",
                    attributes.Keys.Select(key => key)
                );*/
                //syncEvent["pgrowth_syncallattributes"] = syncAll;

                var result = _service.Create(syncEvent);

                syncEvent[$"{syncEntityLogicalName}id"] = result;

                return syncEvent;
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error constructing User Sync Event: {ex}");
                throw;
            }
        }
        public void MarkError(Entity syncEvent, string errorMessage)
        {
            try
            {
                _logger.WriteLog("Marking syncEvent status as error");

                syncEvent["statuscode"] = new OptionSetValue((int)Enum.SyncEventStatusReason.Error);
                syncEvent["pgrowth_errormessage"] = errorMessage;

                _logger.WriteLog("Updating syncEvent");

                _service.Update(syncEvent);

                _logger.WriteLog("syncEvent updated from MarkError");
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Failed to update SyncEvent from MarkError: {0}", ex.ToString());
                if (ex.InnerException != null)
                {
                    _logger.WriteLog($"InnerException: {ex.InnerException}");
                }
                throw;
            }
        }
        public void MarkComplete(Entity syncEvent)
        {
            try
            {
                _logger.WriteLog("Marking syncEvent status as complete");

                syncEvent["statecode"] = new OptionSetValue((int)Enum.Status.Inactive);
                syncEvent["statuscode"] = new OptionSetValue((int)Enum.SyncEventStatusReason.Complete);

                _logger.WriteLog("Updating syncEvent from SyncEventFactory.MarkComplete()");

                _service.Update(syncEvent);

                _logger.WriteLog("_syncEvent updated from MarkComplete");
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error encountered when setting syncEvent status to Inactive and statusReason to Complete: {ex}");
                if (ex.InnerException != null)
                {
                    _logger.WriteLog($"InnerException: {ex.InnerException}");
                }
                throw;
            }
        }
    }
}