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
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.CoreStudent;
        public CoreStudents(IDataverseContext context, IDataverseLoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.Create<CoreStudents>();
        }
        public Entity GetEntityByStudentId(int studentId)
        {
            try
            {
                _logger.WriteLog($"Building GetEntityReferenceByStudentId query expression on {ENTITY_LOGICAL_NAME}");

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

                _logger.WriteLog($"Executing GetEntityReferenceByStudentId query expression on {ENTITY_LOGICAL_NAME}");

                var results = _context.Service.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing ExecuteAlternate query expression");

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"ExecuteAlternate results.Entities[{JsonSerializer.Serialize(
                            results.Entities[0],
                            new JsonSerializerOptions
                            {
                                WriteIndented = true
                            }
                        )}"

                    );
                    return results.Entities[0];
                }
                else
                {
                    _logger.WriteLog("No existing CoreStudents were found");
                    return new Entity();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"CoreStudents.GetEntityByStudentId Exception: {ex}");
                throw;
            }
        }
        public EntityReference GetReferenceByStudentId(int studentId)
        {
            try
            {
                _logger.WriteLog($"Building GetEntityReferenceByStudentId query expression on {ENTITY_LOGICAL_NAME}");

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

                _logger.WriteLog("Executing GetReferenceByStudentId query expression on {ENTITY_LOGICAL_NAME}");

                var results = _context.Service.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing ExecuteAlternate CoreStudents.GetReferenceByStudentId query expression");

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"ExecuteAlternate CoreStudents.GetReferenceByStudentId results.Entities.Count: {results.Entities.Count}");
                    return results.Entities[0].ToEntityReference();
                }
                else
                {
                    _logger.WriteLog("No existing CoreStudents were found");
                    return new EntityReference();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("GetEntityReferenceByStudentId Exception:");
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