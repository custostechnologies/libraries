using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Constants;
using Microsoft.Xrm.Sdk.Messages;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    
    public class EnvironmentVariableValues
    {
        private readonly ITracingService _tracer;
        private readonly IOrganizationService _service;
        public EnvironmentVariableValues(
            ITracingService tracer,
            IOrganizationService service
        )
        {
            _tracer = tracer;
            _service = service;
        }
        public EnvironmentVariable<string> GetValue(string schemaName)
        {
            try
            {
                string value = "";
                string dValue = "";

                _tracer.Trace("Entered EnvironmentVariableValues.GetValue with schemaName: {0}", schemaName);

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

                EntityCollection results = _service.RetrieveMultiple(query);

                if (results.Entities.Count > 0)
                {
                    _tracer.Trace("EnvironmentVariableValues.GetValue query returned results.Entities.Count: {0}", results.Entities.Count);

                    Entity envVar = results.Entities[0];
                    value = envVar.GetAttributeValue<AliasedValue>("value.value")?.Value?.ToString() ?? "";

                    dValue = envVar.GetAttributeValue<string>("defaultvalue");
                    
                    _tracer.Trace("value set to: {0}", value);
                }

                return new EnvironmentVariable<string>()
                {
                    SchemaName = schemaName,
                    Value = value ?? dValue
                };   
            } catch (Exception ex)
            {
                _tracer.Trace("Exception encountered in EnvironmentVariableValues.GetValue(). Exiting. Exception Message: {0}", ex.Message);
                throw;
            }
        }
    }
}