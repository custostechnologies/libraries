using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class Risks
    {
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.Risk;

        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;

        public Risks(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.Create<Risks>();
        }

        public EntityCollection GetItemsPendingTaskAssignment()
        {
            try
            {
                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    ColumnSet = new ColumnSet(
                        RiskFields.RiskId,
                        RiskFields.Name,
                        RiskFields.RiskDescription,
                        RiskFields.TaskSummary,
                        RiskFields.NextReviewDate,
                        RiskFields.NextDueDate,
                        RiskFields.ReviewScheduleNum,
                        RiskFields.ReviewCycleUnits
                    )
                };

                query.Criteria.AddCondition(
                    "statecode",
                    ConditionOperator.Equal,
                    0
                );

                query.Criteria.AddCondition(
                    RiskFields.NextReviewDate,
                    ConditionOperator.OnOrBefore,
                    DateTime.UtcNow
                );

                query.Criteria.AddCondition(
                    RiskFields.NextDueDate,
                    ConditionOperator.NotNull
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
                    "Exception in Risks.GetItemsPendingTaskAssignment: {0}",
                    ex
                );

                throw;
            }
        }
    }
}