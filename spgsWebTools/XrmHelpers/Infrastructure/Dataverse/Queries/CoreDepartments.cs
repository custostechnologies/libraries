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
            try
            {
                _tracer.Trace("Building GetReferenceByCode query expression on {0}", ENTITY_LOGICAL_NAME);

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
            catch (Exception ex)
            {
                _tracer.Trace("Exception in CoreDepartments.GetReferenceByCode: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in CoreDepartments.GetReferenceByCode: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        public EntityReference GetReferenceById(Guid id)
        {
            try
            {
                _tracer.Trace("Building GetReferenceById query expression on {0}", ENTITY_LOGICAL_NAME);

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
            catch (Exception ex)
            {
                _tracer.Trace("Exception in CoreDepartments.GetReferenceById: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in CoreDepartments.GetReferenceById: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
    }
}