using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class SyncEventFactory
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly string _correlationId;
        public SyncEventFactory(
            IDataverseContext context,
            string correlationId
        )
        {
            _context = context;
            _correlationId = correlationId;

            _logger = _context.GetLogger<SyncEventFactory>();
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
                syncEvent["statecode"] = new OptionSetValue((int)Status.Active);
                syncEvent["statuscode"] = new OptionSetValue((int)SyncEventStatusReason.Pending);
                syncEvent["pgrowth_sourcerecordid"] = synMirrorId;
                syncEvent["pgrowth_correlationid"] = _correlationId;
                //syncEvent["pgrowth_exitearly"] = exitEarly;
                /*syncEvent["pgrowth_updatedattributes"] = string.Join(
                    ",",
                    attributes.Keys.Select(key => key)
                );*/
                //syncEvent["pgrowth_syncallattributes"] = syncAll;

                var result = _context.Runtime.UserService.Create(syncEvent);

                syncEvent[$"{syncEntityLogicalName}id"] = result;

                return syncEvent;
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error constructing User Sync Event: ", ex);
                throw;
            }
        }
        public void MarkError(Entity syncEvent, string errorMessage)
        {
            try
            {
                _logger.WriteLog("Marking syncEvent status as error");

                syncEvent["statuscode"] = new OptionSetValue((int)SyncEventStatusReason.Error);
                syncEvent["pgrowth_errormessage"] = errorMessage;

                _logger.WriteLog("Updating syncEvent");

                _context.Runtime.UserService.Update(syncEvent);

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

                syncEvent["statecode"] = new OptionSetValue((int)Status.Inactive);
                syncEvent["statuscode"] = new OptionSetValue((int)SyncEventStatusReason.Complete);

                _logger.WriteLog("Updating syncEvent from SyncEventFactory.MarkComplete()");

                _context.Runtime.UserService.Update(syncEvent);

                _logger.WriteLog("_syncEvent updated from MarkComplete");
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error encountered when setting syncEvent status to Inactive and statusReason to Complete: ", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog($"InnerException: {ex.InnerException}");
                }
                throw;
            }
        }
    }
}