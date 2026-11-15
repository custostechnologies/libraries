using Microsoft.Extensions.Options;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Schemas;
using Enums = Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Infrastructure.Dataverse.Factories;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class PayClaimForms
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        public PayClaimForms(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.Create<PayClaimForms>();
        }
        public EntityCollection GetForms()
        {
            _logger.WriteLog("Building GetReferenceBySeq query expression on {0}", EntityLogicalNames.PayClaimForm);

            var query = new QueryExpression(EntityLogicalNames.PayClaimForm)
            {
                ColumnSet = PayClaimFormSchema.SyncColumns
            };

            _logger.WriteLog($"Executing query expression on {EntityLogicalNames.PayClaimForm}");

            var results = _context.Runtime.UserService.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return results;
            }
            else
            {
                _logger.WriteLog("No existing entities were found");
                return new EntityCollection();
            }
        }
    }
}