using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.NewUserSyncEvent.Queries
{
    public class CheckUserRoleQueryService
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private Entity _role = new Entity("role");
        public CheckUserRoleQueryService(IDataverseContext context, IDataverseLogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public bool HasRole(Guid systemuserid, string roleName)
        {
            QueryExpression roleQuery = new QueryExpression
            {
                EntityName = "role",
                ColumnSet = new ColumnSet("roleid"),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "name",
                            ConditionOperator.Equal,
                            roleName
                        )
                    }
                }
            };

            EntityCollection roles = _context.Service.RetrieveMultiple(roleQuery);

            if (roles.Entities.Count > 0)
            {
                _role = roles.Entities[0];

                _logger.WriteLog("Found role: {0}", roleName);
                _logger.WriteLog("Checking for association with given user");

                LinkEntity systemUserRoleLink = new LinkEntity()
                {
                    LinkFromEntityName = "role",
                    LinkFromAttributeName = "roleid",
                    LinkToEntityName = "systemuserroles",
                    LinkToAttributeName = "roleid",
                    LinkCriteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                "systemuserid",
                                ConditionOperator.Equal,
                                systemuserid
                            )
                        }
                    }
                };

                QueryExpression userHasRoleQuery = new QueryExpression("role")
                {
                    ColumnSet = new ColumnSet("roleid"),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                "name",
                                ConditionOperator.Equal,
                                roleName
                            )
                        }
                    },
                    LinkEntities =
                    {
                        systemUserRoleLink
                    }
                };

                EntityCollection results = _context.Service.RetrieveMultiple(userHasRoleQuery);

                return results.Entities.Count > 0;
            }
            else
            {
                _logger.WriteLog("Unable to find role.");
                throw new InvalidPluginExecutionException();
            }
        }
    }
}