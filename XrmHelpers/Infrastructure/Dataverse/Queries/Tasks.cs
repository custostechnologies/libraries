using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Enums;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class Tasks
    {
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.Task;

        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;

        public Tasks(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.Create<Risks>();
        }

        public int GetOpenTasksCount(EntityReference regardingRef)
        {
            try
            {
                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    ColumnSet = new ColumnSet(
                        TaskFields.TaskId,
                        TaskFields.ActivityStatus,
                        TaskFields.StatusReason,
                        TaskFields.Regarding
                    )
                };

                query.Criteria.AddCondition(
                    "statecode",
                    ConditionOperator.Equal,
                    (int)ActivityStatus.Open
                );

                query.Criteria.AddCondition(
                    TaskFields.Regarding,
                    ConditionOperator.Equal,
                    regardingRef.Id
                );

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                return results.Entities.Count;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(
                    "Exception in Risks.GetOpenTasksCount: {0}",
                    ex
                );

                throw;
            }
        }
    }
}