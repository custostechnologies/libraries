using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class CoreStaffCategories
    {
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_corestaffcategory";
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        public CoreStaffCategories(IDataverseContext context, IDataverseLogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public EntityReference GetReferenceByCode(string code)
        {
            try
            {
                _logger.WriteLog("Building query expression on {0}", ENTITY_LOGICAL_NAME);

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

                _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _context.Service.RetrieveMultiple(query);

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
                _logger.WriteLog("Exception in CoreStaffCategories.GetReferenceByCode: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception in CoreStaffCategories.GetReferenceByCode: {0}", ex.InnerException.Message);
                }
                throw;
            }


        }
    }
}