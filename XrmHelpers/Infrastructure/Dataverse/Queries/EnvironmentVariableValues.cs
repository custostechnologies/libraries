using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Constants;
using Microsoft.Xrm.Sdk.Messages;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{

    public class EnvironmentVariableValues
    {
        private readonly IDataverseLogger _logger;
        private readonly IDataverseContext _context;
        public EnvironmentVariableValues(
            IDataverseContext context,
          IDataverseLogger logger
        )
        {
            _logger = logger;
            _context = context;
        }
        public EnvironmentVariable<string> GetValue(string schemaName)
        {
            try
            {
                string value = "";
                string dValue = "";

                _logger.WriteLog($"Entered EnvironmentVariableValues.GetValue with schemaName: {schemaName}");

                var query = new QueryExpression(EntityLogicalNames.EnvironmentVariableDefinition)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet("schemaname", "defaultvalue"),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                "schemaname",
                                ConditionOperator.Equal,
                                schemaName
                            )
                        }
                    }
                };

                var link = new LinkEntity
                {
                    LinkFromEntityName = EntityLogicalNames.EnvironmentVariableDefinition,
                    LinkToEntityName = EntityLogicalNames.EnvironmentVariableValue,
                    LinkFromAttributeName = "environmentvariabledefinitionid",
                    LinkToAttributeName = "environmentvariabledefinitionid",
                    JoinOperator = JoinOperator.LeftOuter,
                    Columns = new ColumnSet("value"),
                    EntityAlias = "value"
                };

                query.LinkEntities.Add(link);

                EntityCollection results = _context.Service.RetrieveMultiple(query);

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"EnvironmentVariableValues.GetValue query returned results.Entities.Count: {results.Entities.Count}");

                    Entity envVar = results.Entities[0];
                    value = envVar.GetAttributeValue<AliasedValue>("value.value")?.Value?.ToString() ?? "";

                    dValue = envVar.GetAttributeValue<string>("defaultvalue");

                    _logger.WriteLog($"value set to: {value}");
                }

                return new EnvironmentVariable<string>()
                {
                    SchemaName = schemaName,
                    Value = value ?? dValue
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception encountered in EnvironmentVariableValues.GetValue(). Exiting. Exception Message: {ex}");
                throw;
            }
        }
    }
}