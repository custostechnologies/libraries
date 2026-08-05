using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class CoreJobPositions
    {
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_corejobposition";
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        public CoreJobPositions(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public EntityReference GetReferenceBySeq(int seq)
        {
            _tracer.Trace("Building GetReferenceBySeq query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    "pgrowth_jobpositionseq",
                    "pgrowth_sourceoftruthid",
                    "pgrowth_corejobpositionid"

                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "pgrowth_jobpositionseq",
                            ConditionOperator.Equal,
                            seq
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
                    "pgrowth_jobpositionseq",
                    "pgrowth_sourceoftruthid",
                    "pgrowth_corejobpositionid"

                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "pgrowth_corejobpositionid",
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