using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Domain.Schemas;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class CoreStaffMembers
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_corestaffmember";
        public CoreStaffMembers(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public Entity GetEntityUsingTruth(string truthId)
        {
            try
            {
                _tracer.Trace("Building ExecuteAlternate query expression on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = CoreStaffMemberSchema.SyncColumns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                "pgrowth_sourceoftruthid",
                                ConditionOperator.Equal,
                                truthId
                            )
                        }
                    }
                };

                _tracer.Trace("Executing ExecuteAlternate query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing ExecuteAlternate query expression");

                if (results.Entities.Count > 0)
                {
                    _tracer.Trace("ExecuteAlternate results.Entities[0]: ",
                        JsonSerializer.Serialize(
                            results.Entities[0],
                            new JsonSerializerOptions
                            {
                                WriteIndented = true
                            }
                        )
                    );
                    return results.Entities[0];
                }
                else
                {
                    _tracer.Trace("No existing entities were found");
                    return new Entity(ENTITY_LOGICAL_NAME);
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("ExecuteAlternate Exception:");
                _tracer.Trace(ex.ToString());
                if (ex.InnerException != null)
                {
                    _tracer.Trace("Inner Exception:");
                    _tracer.Trace(ex.InnerException.ToString());
                }
                throw;
            }
        }
        public EntityReference GetEntityReferenceByStaffId(int staffId)
        {
            try
            {
                _tracer.Trace("Building GetEntityReferenceByStaffId query expression on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = CoreStaffMemberSchema.SyncColumns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                "pgrowth_staffid",
                                ConditionOperator.Equal,
                                staffId
                            )
                        }
                    }
                };

                _tracer.Trace("Executing GetEntityReferenceByStaffId query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing ExecuteAlternate query expression");

                if (results.Entities.Count > 0)
                {
                    _tracer.Trace("ExecuteAlternate results.Entities[0]: ",
                        JsonSerializer.Serialize(
                            results.Entities[0],
                            new JsonSerializerOptions
                            {
                                WriteIndented = true
                            }
                        )
                    );
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
                _tracer.Trace("GetEntityReferenceByStaffId Exception:");
                _tracer.Trace(ex.ToString());
                if (ex.InnerException != null)
                {
                    _tracer.Trace("Inner Exception:");
                    _tracer.Trace(ex.InnerException.ToString());
                }
                throw;
            }
        }
    }
}