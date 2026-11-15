using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using custos.XrmHelpers.Constants;


namespace custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class AutoNumberCounters
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        public AutoNumberCounters(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.Create<AutoNumberCounters>();
        }
        public string ReserveIndex(string tableLogicalName)
        {
            // TODO: move counter record UPDATE to allow complete rollback upon exception
            try
            {
                _logger.WriteLog("Executing ReserveIndex query expression on {0}", EntityLogicalNames.AutoNumberCounter);

                var cols = new ColumnSet(
                    AutoNumberCounterFields.AutoNumberCounterId,
                    AutoNumberCounterFields.CounterKey,
                    AutoNumberCounterFields.EnvironmentName,
                    AutoNumberCounterFields.Prefix,
                    AutoNumberCounterFields.Value
                );

                var reference = new EntityReference(
                    EntityLogicalNames.AutoNumberCounter,
                    AutoNumberCounterFields.CounterKey,
                    tableLogicalName
                );

                _logger.WriteLog("Building retrieve request for current counter value and prefix.");

                RetrieveRequest retrieveCounter = new RetrieveRequest
                {
                    Target = reference,
                    ColumnSet = cols
                };

                _logger.WriteLog("Executing retrieve request for current counter value and prefix.");

                RetrieveResponse response = (RetrieveResponse)_context.Runtime.UserService.Execute(retrieveCounter);

                if (response.Entity == null)
                {
                    var invalidResponses = new Exception("None or multiple counter rows found.");
                    _logger.WriteLog(
                        invalidResponses,
                        $"retrieveResponse on table: {tableLogicalName} for record with alternate key (counterKey) = {EntityLogicalNames.AutoNumberCounter} returned null");
                    throw invalidResponses;
                }

                _logger.WriteLog("Retrieve request complete.");

                Entity counterUpdate = response.Entity;

                int currentCounterValue = counterUpdate.GetAttributeValue<int>(AutoNumberCounterFields.Value);
                string prefix = counterUpdate.GetAttributeValue<string>(AutoNumberCounterFields.Prefix);

                _logger.WriteLog($"Current Value: {currentCounterValue} and prefix: {prefix}.");

                counterUpdate[AutoNumberCounterFields.Value] = currentCounterValue + 1;

                _logger.WriteLog($"Incrementing counter value to {currentCounterValue + 1}");

                string paddedCounter = currentCounterValue.ToString().PadLeft(4, '0');

                _context.Runtime.UserService.Update(counterUpdate);
                return $"{prefix}-{paddedCounter}";
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex, "Exception: ");
                throw;
            }
        }
    }
}