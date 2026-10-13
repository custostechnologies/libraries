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
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_corestaffmember";
        public CoreStaffMembers(IDataverseContext context, IDataverseLoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.Create<CoreStaffMembers>();
        }
        public Entity GetEntityByTruth(string truthId)
        {
            try
            {
                _logger.WriteLog($"Building ExecuteAlternate CoreStaffMembers.GetEntityByTruth query expression on {ENTITY_LOGICAL_NAME}");

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

                _logger.WriteLog($"Executing ExecuteAlternate CoreStaffMembers.GetEntityByTruth query expression on {ENTITY_LOGICAL_NAME}");

                var results = _context.Service.RetrieveMultiple(query);

                _logger.WriteLog($"Finished executing ExecuteAlternate CoreStaffMembers.GetEntityByTruth query expression, results.Entities.Count: {results.Entities.Count}");

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"ExecuteAlternate results.Entities[0]: {JsonSerializer.Serialize(
                            results.Entities[0],
                            new JsonSerializerOptions
                            {
                                WriteIndented = true
                            }
                        )}");
                    return results.Entities[0];
                }
                else
                {
                    _logger.WriteLog("No existing CoreStaffMembers were found");
                    return new Entity();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("ExecuteAlternate Exception:");
                _logger.WriteLog(ex.ToString());
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception:");
                    _logger.WriteLog(ex.InnerException.ToString());
                }
                throw;
            }
        }
        public Entity GetEntityByStaffId(int staffId)
        {
            try
            {
                _logger.WriteLog($"Building GetEntityReferenceByStaffId query expression on {ENTITY_LOGICAL_NAME}");

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

                _logger.WriteLog($"Executing GetEntityReferenceByStaffId query expression on {ENTITY_LOGICAL_NAME}");

                var results = _context.Service.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing ExecuteAlternate query expression");

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"ExecuteAlternate results.Entities[0]: {JsonSerializer.Serialize(
                            results.Entities[0],
                            new JsonSerializerOptions
                            {
                                WriteIndented = true
                            }
                        )}");
                    return results.Entities[0];
                }
                else
                {
                    _logger.WriteLog("No existing CoreStaffMembers were found");
                    return new Entity();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"CoreStaffMembers.GetEntityByStaffId Exception: {ex}");
                throw;
            }
        }
        public EntityReference GetReferenceByStaffId(int staffId)
        {
            try
            {
                _logger.WriteLog($"Building GetEntityReferenceByStaffId query expression on {ENTITY_LOGICAL_NAME}");

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

                _logger.WriteLog($"Executing GetReferenceByStaffId query expression on {ENTITY_LOGICAL_NAME}");

                var results = _context.Service.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing ExecuteAlternate CoreStaffMembers.GetReferenceByStaffId query expression");

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"ExecuteAlternate CoreStaffMembers.GetReferenceByStaffId results.Entities.Count: {results.Entities.Count}");
                    return results.Entities[0].ToEntityReference();
                }
                else
                {
                    _logger.WriteLog("No existing CoreStaffMembers were found");
                    return new EntityReference();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("GetEntityReferenceByStaffId Exception:");
                _logger.WriteLog(ex.ToString());
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception:");
                    _logger.WriteLog(ex.InnerException.ToString());
                }
                throw;
            }
        }
    }
}