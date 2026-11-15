using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Shared.Utilities;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class BusinessUnits
    {
        private readonly string ENTITY_LOGICAL_NAME = "businessunits";
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        public BusinessUnits(IDataverseContext context, IDataverseLoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.Create<BusinessUnits>();
        }
        public EntityReference GetReferenceByCode(string code)
        {
            try
            {
                var safe = SafeString.Normalise(code);
                var norm = safe.ToUpper();
                // convert back to upper case to match BU Code column format

                _logger.WriteLog("Building BusinessUnits.GetByCode query expression on {0}", ENTITY_LOGICAL_NAME);

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
                _logger.WriteLog("Exception in BusinessUnits.GetReferenceByCode: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception in BusinessUnits.GetReferenceByCode: {0}", ex.InnerException.Message);
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
                _logger.WriteLog("Exception in BusinessUnits.GetReferenceById: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception in BusinessUnits.GetReferenceById: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
    }
}