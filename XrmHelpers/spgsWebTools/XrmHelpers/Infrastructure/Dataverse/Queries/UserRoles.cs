using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.NewUserSyncEvent.Queries
{
    public class CheckUserRoleQueryService
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private Entity _role = new Entity("role");
        public CheckUserRoleQueryService(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
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

            EntityCollection roles = _service.RetrieveMultiple(roleQuery);

            if (roles.Entities.Count > 0)
            {
                _role = roles.Entities[0];

                _tracer.Trace("Found role: {0}", roleName);
                _tracer.Trace("Checking for association with given user");

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

                EntityCollection results = _service.RetrieveMultiple(userHasRoleQuery);

                return results.Entities.Count > 0;
            }
            else
            {
                _tracer.Trace("Unable to find role.");
                throw new InvalidPluginExecutionException();
            }
        }
    }
}