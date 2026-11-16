using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class Hazards
    {
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.Hazard;

        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;

        public Hazards(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.Create<Hazards>();
        }

        public EntityCollection GetHazardsPendingTaskAssignment()
        {
            try
            {
                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    ColumnSet = new ColumnSet(
                        HazardFields.HazardId,
                        HazardFields.Name,
                        HazardFields.Description,
                        HazardFields.TaskSummary,
                        HazardFields.NextReviewDate,
                        HazardFields.NextDueDate,
                        HazardFields.ReviewCycle,
                        HazardFields.ReviewCycleUnits
                    )
                };

                query.Criteria.AddCondition(
                    "statecode",
                    ConditionOperator.Equal,
                    0
                );

                query.Criteria.AddCondition(
                    HazardFields.RecurringSubTasks,
                    ConditionOperator.Equal,
                    true
                );

                query.Criteria.AddCondition(
                    HazardFields.NextReviewDate,
                    ConditionOperator.OnOrBefore,
                    DateTime.UtcNow
                );

                var ownerLink = query.AddLink(
                    "team",
                    "ownerid",
                    "teamid",
                    JoinOperator.Inner
                );

                ownerLink.EntityAlias = "assignedteam";

                ownerLink.Columns = new ColumnSet(
                    "teamid",
                    "name"
                );

                return _context.Runtime.UserService.RetrieveMultiple(query);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(
                    "Exception in Hazards.GetItemsPendingTaskAssignment: {0}",
                    ex
                );

                throw;
            }
        }
    }
}