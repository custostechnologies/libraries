using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Contracts;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class Policies : IReviewQuery
    {
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.Policy;

        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;

        public Policies(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.Create<Policies>();
        }

        public EntityCollection GetItemsPendingTaskAssignment()
        {
            try
            {
                _logger.WriteLog(
                    "Building GetItemsPendingTaskAssignment query expression on {0}",
                    ENTITY_LOGICAL_NAME
                );

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    ColumnSet = new ColumnSet(
                        PolicyFields.PolicyId,
                        PolicyFields.Title,
                        PolicyFields.Description,
                        PolicyFields.TaskSummary,
                        PolicyFields.NextReviewDate,
                        PolicyFields.NextDueDate,
                        PolicyFields.ReviewCycle,
                        PolicyFields.ReviewCycleUnits
                    )
                };

                query.Criteria.AddCondition(
                    "statecode",
                    ConditionOperator.Equal,
                    0
                );

                query.Criteria.AddCondition(
                    PolicyFields.RecurringSubTasks,
                    ConditionOperator.Equal,
                    true
                );

                query.Criteria.AddCondition(
                    PolicyFields.NextReviewDate,
                    ConditionOperator.OnOrBefore,
                    DateTime.UtcNow
                );

                var intersect = query.AddLink(
                    "pgrowth_policy_team",
                    PolicyFields.PolicyId,
                    PolicyFields.PolicyId,
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
                    "Exception in Policies.GetItemsPendingTaskAssignment: {0}",
                    ex
                );

                throw;
            }
        }
    }
}