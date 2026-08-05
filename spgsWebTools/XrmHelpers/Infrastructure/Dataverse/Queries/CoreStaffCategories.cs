using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class CoreStaffCategories
    {
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_corestaffcategory";
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        public CoreStaffCategories(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public EntityReference GetReferenceByCode(string code)
        {
            _tracer.Trace("Building query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    "pgrowth_code",
                    "pgrowth_sourceoftruthid",
                    "pgrowth_corestaffcategoryid"

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
    }
}