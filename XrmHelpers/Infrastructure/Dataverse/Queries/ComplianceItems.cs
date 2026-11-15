using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using custos.XrmHelpers.Constants;

namespace custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class ComplianceItems
    {
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_complianceitem";
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        public ComplianceItems(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory
                )
        {
            _context = context;
            _logger = loggerFactory.Create<CoreDepartments>();
        }
        public EntityCollection GetItemsPendingTaskAssignment()
        {
            try
            {
                _logger.WriteLog("Building GetItemsPendingTaskAssignment query expression on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet(
                        ComplianceItemFields.ChildTaskType,
                        ComplianceItemFields.ComplianceItemId,
                        ComplianceItemFields.Description,
                        ComplianceItemFields.Name,
                        ComplianceItemFields.NextDueDate,
                        ComplianceItemFields.NextReviewDate,
                        "pgrowth_reviewcycle",
                        "pgrowth_reviewcycleunits"
                    )
                };

                query.Criteria.AddCondition(
                    "statecode",
                    ConditionOperator.Equal,
                    0
                );

                query.Criteria.AddCondition(
                    ComplianceItemFields.RecurringSubTasks,
                    ConditionOperator.Equal,
                    true
                );

                query.Criteria.AddCondition(
                    ComplianceItemFields.NextReviewDate,
                    ConditionOperator.OnOrBefore,
                    DateTime.UtcNow
                );

                var intersect = query.AddLink(
                    "pgrowth_complianceitem_team_nn",
                    ComplianceItemFields.ComplianceItemId,
                    ComplianceItemFields.ComplianceItemId,
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
                _logger.WriteLog("Exception in CoreDepartments.GetReferenceByCode: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception in CoreDepartments.GetReferenceByCode: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        public EntityReference GetReferenceById(Guid id)
        {
            try
            {
                _logger.WriteLog("Building GetReferenceById query expression on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet(
                        "pgrowth_code",
                        "pgrowth_sourceoftruthid",
                        "pgrowth_coredepartmentid"

                    ),
                    Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "pgrowth_coredepartmentid",
                            ConditionOperator.Equal,
                            id
                        )
                    }
                }
                };

                _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return new EntityReference(ENTITY_LOGICAL_NAME, results.Entities[0].Id);
                }
                else
                {
                    _logger.WriteLog("No existing entities were found");
                    return new EntityReference(ENTITY_LOGICAL_NAME);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Exception in CoreDepartments.GetReferenceById: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception in CoreDepartments.GetReferenceById: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
    }
}