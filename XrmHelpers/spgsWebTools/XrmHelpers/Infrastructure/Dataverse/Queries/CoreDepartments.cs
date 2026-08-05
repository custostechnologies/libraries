using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class CoreDepartments
    {
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_coredepartment";
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        public CoreDepartments(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public EntityReference GetReferenceByCode(string code)
        {
            _tracer.Trace("Building GetReferenceByCode query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    "pgrowth_code",
                    "pgrowth_sourceoftruthid",
                    "pgrowth_coredepartmentid",
                    "pgrowth_coredepartmentname"

                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "pgrowth_code",
                            ConditionOperator.Equal,
                            code
                        )
                    }
                }
            };

            _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _service.RetrieveMultiple(query);

            _tracer.Trace("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return new EntityReference(ENTITY_LOGICAL_NAME, results.Entities[0].Id);
            }
            else
            {
                _tracer.Trace("No existing entities were found");
                return new EntityReference(ENTITY_LOGICAL_NAME);
            }
        }
        public EntityReference GetReferenceById(Guid id)
        {
            _tracer.Trace("Building GetReferenceById query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    "pgrowth_code",
                    "pgrowth_sourceoftruthid",
                    "pgrowth_coredepartmentid",
                    "pgrowth_coredepartmentname"

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

            _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _service.RetrieveMultiple(query);

            _tracer.Trace("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return new EntityReference(ENTITY_LOGICAL_NAME, results.Entities[0].Id);
            }
            else
            {
                _tracer.Trace("No existing entities were found");
                return new EntityReference(ENTITY_LOGICAL_NAME);
            }
        }
    }
}