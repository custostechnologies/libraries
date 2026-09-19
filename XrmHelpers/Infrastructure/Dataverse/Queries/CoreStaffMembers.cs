using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
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
        public Entity GetEntityByTruth(string truthId)
        {
            try
            {
<<<<<<< HEAD
                _tracer.Trace("Building ExecuteAlternate query expression on {0}", ENTITY_LOGICAL_NAME);
=======
                _tracer.Trace("Building ExecuteAlternate CoreStaffMembers.GetEntityByTruth query expression on {0}", ENTITY_LOGICAL_NAME);
>>>>>>> source-temp/refactor/decouple-xrmhelpers

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = CoreStaffMemberSchema.SyncColumns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                CoreStaffMemberFields.SourceOfTruthId,
                                ConditionOperator.Equal,
                                truthId
                            )
                        }
                    }
                };

<<<<<<< HEAD
                _tracer.Trace("Executing ExecuteAlternate query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing ExecuteAlternate query expression");
=======
                _tracer.Trace("Executing ExecuteAlternate CoreStaffMembers.GetEntityByTruth query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing ExecuteAlternate CoreStaffMembers.GetEntityByTruth query expression, results.Entities.Count: {0}", results.Entities.Count);
>>>>>>> source-temp/refactor/decouple-xrmhelpers

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
<<<<<<< HEAD
                    _tracer.Trace("No existing entities were found");
                    return new Entity(ENTITY_LOGICAL_NAME);
=======
                    _tracer.Trace("No existing CoreStaffMembers were found");
                    return null;
>>>>>>> source-temp/refactor/decouple-xrmhelpers
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
        public Entity GetEntityByStaffId(int staffId)
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
                                CoreStaffMemberFields.StaffId,
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
                    return results.Entities[0];
                }
                else
                {
<<<<<<< HEAD
                    _tracer.Trace("No existing entities were found");
                    return new Entity(ENTITY_LOGICAL_NAME);
=======
                    _tracer.Trace("No existing CoreStaffMembers were found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("CoreStaffMembers.GetEntityByStaffId Exception: {0}", ex.Message);
                throw;
            }
        }
        public EntityReference GetReferenceByStaffId(int staffId)
        {
            try
            {
                _tracer.Trace("Building GetEntityReferenceByStaffId query expression on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet(
                        CoreStaffMemberFields.StaffId
                    ),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                CoreStaffMemberFields.StaffId,
                                ConditionOperator.Equal,
                                staffId
                            )
                        }
                    }
                };

                _tracer.Trace("Executing GetReferenceByStaffId query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing ExecuteAlternate CoreStaffMembers.GetReferenceByStaffId query expression");

                if (results.Entities.Count > 0)
                {
                    _tracer.Trace("ExecuteAlternate CoreStaffMembers.GetReferenceByStaffId results.Entities.Count: ", results.Entities.Count);
                    return results.Entities[0].ToEntityReference();
                }
                else
                {
                    _tracer.Trace("No existing CoreStaffMembers were found");
                    return null;
>>>>>>> source-temp/refactor/decouple-xrmhelpers
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