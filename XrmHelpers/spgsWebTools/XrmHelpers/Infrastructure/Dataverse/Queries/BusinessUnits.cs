using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Shared.Utilities;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class BusinessUnits
    {
        private readonly string ENTITY_LOGICAL_NAME = "businessunits";
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        public BusinessUnits(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public EntityReference GetReferenceByCode(string code)
        {
            try
            {
                var safe = SafeString.Normalise(code);
                var norm = safe.ToUpper();
                // convert back to upper case to match BU Code column format

                _tracer.Trace("Building BusinessUnits.GetByCode query expression on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet(
                        BusinessUnitFields.BUCode,
                        BusinessUnitFields.BusinessUnitId

                    ),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                BusinessUnitFields.BUCode,
                                ConditionOperator.Equal,
                                safe
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
                _tracer.Trace("Exception in BusinessUnits.GetReferenceByCode: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in BusinessUnits.GetReferenceByCode: {0}", ex.InnerException.Message);
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
                        BusinessUnitFields.BUCode,
                        BusinessUnitFields.BusinessUnitId

                    ),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                BusinessUnitFields.BusinessUnitId,
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
                _tracer.Trace("Exception in BusinessUnits.GetReferenceById: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in BusinessUnits.GetReferenceById: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
    }
}