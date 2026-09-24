using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class AutoNumberCounters
    {
        private readonly IDataverseContext _context;
        private readonly DataverseLogger _logger;
        public AutoNumberCounters(
            IDataverseContext context
        )
        {
            _context = context;
            _logger = new DataverseLogger(
                _context.Logger,
                nameof(AutoNumberCounters)
            );
        }
        public string ReserveIndex(string tableLogicalName)
        {
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

                ExecuteTransactionResponse retrieveResponse = (ExecuteTransactionResponse)_context.Service.Execute(retrieveCounter);

                if (retrieveResponse.Responses.Count != 1)
                {
                    var invalidResponses = new Exception("None or multiple counter rows found.");
                    _logger.WriteLog(
                        invalidResponses,
                        "retrieveResponse on table: {TableName} for record with alternate key (counterKey) = {CounterKey} returned {Count} responses.",
                        EntityLogicalNames.AutoNumberCounter,
                        tableLogicalName,
                        retrieveResponse.Responses.Count
                    );
                    throw invalidResponses;
                }

                _logger.WriteLog("Retrieve request complete.");

                var response = (RetrieveResponse)retrieveResponse.Responses[0];
                Entity counterUpdate = response.Entity;

                int currentCounterValue = counterUpdate.GetAttributeValue<int>(AutoNumberCounterFields.Value);
                string prefix = counterUpdate.GetAttributeValue<string>(AutoNumberCounterFields.Prefix);

                _logger.WriteLog($"Current Value: {currentCounterValue} and prefix: {prefix}.");

                counterUpdate[AutoNumberCounterFields.Value] = currentCounterValue + 1;

                _logger.WriteLog($"Incrementing counter value to {currentCounterValue + 1}");

                string paddedCounter = currentCounterValue.ToString().PadLeft(4, '0');

                _context.Service.Update(counterUpdate);
                return $"{prefix}-{paddedCounter}";
            }
            catch (Exception ex)
            {
                _logger.WriteLog(
                    ex,
                    "Exception detected when executing {NameOf} on table {TableName}.",
                    nameof(ReserveIndex),
                    tableLogicalName
                );
                throw;
            }
        }
    }
}