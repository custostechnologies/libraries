using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Contracts;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class Hazards : IReviewQuery
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

        public EntityCollection GetItemsPendingTaskAssignment()
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

                var intersect = query.AddLink(
                    "pgrowth_hazard_team_nn",
                    HazardFields.HazardId,
                    HazardFields.HazardId,
                    JoinOperator.Inner
                );

                var teamLink = intersect.AddLink(
                    "team",
                    "teamid",
                    "teamid",
                    JoinOperator.Inner
                );

                teamLink.EntityAlias = "assignedteam";

                teamLink.Columns = new ColumnSet(
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