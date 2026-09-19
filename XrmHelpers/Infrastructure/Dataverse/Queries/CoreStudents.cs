using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Schemas;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class CoreStudents
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.CoreStudent;
        public CoreStudents(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public Entity GetEntityByStudentId(int studentId)
        {
            try
            {
                _tracer.Trace("Building GetEntityReferenceByStudentId query expression on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = CoreStudentSchema.SyncColumns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                CoreStudentFields.StudentId,
                                ConditionOperator.Equal,
                                studentId
                            )
                        }
                    }
                };

                _tracer.Trace("Executing GetEntityReferenceByStudentId query expression on {0}", ENTITY_LOGICAL_NAME);

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
                    _tracer.Trace("No existing CoreStudents were found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("CoreStudents.GetEntityByStudentId Exception: {0}", ex.Message);
                throw;
            }
        }
        public EntityReference GetReferenceByStudentId(int studentId)
        {
            try
            {
                _tracer.Trace("Building GetEntityReferenceByStudentId query expression on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet(
                        CoreStudentFields.StudentId
                    ),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                CoreStudentFields.StudentId,
                                ConditionOperator.Equal,
                                studentId
                            )
                        }
                    }
                };

                _tracer.Trace("Executing GetReferenceByStudentId query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing ExecuteAlternate CoreStudents.GetReferenceByStudentId query expression");

                if (results.Entities.Count > 0)
                {
                    _tracer.Trace("ExecuteAlternate CoreStudents.GetReferenceByStudentId results.Entities.Count: ", results.Entities.Count);
                    return results.Entities[0].ToEntityReference();
                }
                else
                {
                    _tracer.Trace("No existing CoreStudents were found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("GetEntityReferenceByStudentId Exception:");
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