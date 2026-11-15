using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Schemas;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class CoreStudents
    {
        public class CoreStudentMetadata
        {
            public string First { get; set; } = string.Empty;
            public string Last { get; set; } = string.Empty;
            public string Full { get; set; } = string.Empty;
            public int StudentId { get; set; }
            public string Email { get; set; } = string.Empty;
            public Guid CoreStudentId { get; set; }
        }
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

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

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
        public Entity? GetNullableEntity(int studentId)
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

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

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
                    return null;
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

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

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
        public CoreStudentMetadata GetMetadataByStudentId(int studentId)
        {
            try
            {
                _logger.WriteLog($"Building GetMetadataByStudentId query expression on {ENTITY_LOGICAL_NAME}");

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet(
                        CoreStudentFields.StudentId,
                        CoreStudentFields.DisplayName,
                        CoreStudentFields.Given1,
                        CoreStudentFields.Surname,
                        CoreStudentFields.EmailAddress,
                        CoreStudentFields.CoreStudentId
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

                _logger.WriteLog($"Executing {nameof(GetMetadataByStudentId)} query expression on {ENTITY_LOGICAL_NAME}");

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing ExecuteAlternate CoreStaffMembers.GetReferenceByStaffId query expression");

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"ExecuteAlternate {nameof(GetMetadataByStudentId)} results.Entities.Count: {results.Entities.Count}");

                    var entity = results.Entities.First();

                    return new CoreStudentMetadata
                    {
                        First = entity.GetAttributeValue<string>(CoreStudentFields.Given1),
                        Last = entity.GetAttributeValue<string>(CoreStudentFields.Surname),
                        Full = entity.GetAttributeValue<string>(CoreStudentFields.DisplayName),
                        Email = entity.GetAttributeValue<string>(CoreStudentFields.EmailAddress),
                        StudentId = entity.GetAttributeValue<int>(CoreStudentFields.StudentId),
                        CoreStudentId = entity.GetAttributeValue<Guid>(CoreStudentFields.CoreStudentId)
                    };
                }
                else
                {
                    throw new InvalidPluginExecutionException("No existing CoreStudents were found");
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"{nameof(GetMetadataByStudentId)} Exception: ", ex);
                throw;
            }
        }
        public CoreStudentMetadata GetMetadataByOrgEmail(string email)
        {
            try
            {
                _logger.WriteLog($"Building GetMetadataByStudentId query expression on {ENTITY_LOGICAL_NAME}");

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet(
                        CoreStudentFields.StudentId,
                        CoreStudentFields.DisplayName,
                        CoreStudentFields.Given1,
                        CoreStudentFields.Surname,
                        CoreStudentFields.EmailAddress,
                        CoreStudentFields.CoreStudentId
                    ),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                CoreStudentFields.EmailAddress,
                                ConditionOperator.Equal,
                                email
                            )
                        }
                    }
                };

                _logger.WriteLog($"Executing {nameof(GetMetadataByOrgEmail)} query expression on {ENTITY_LOGICAL_NAME}");

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"ExecuteAlternate {nameof(GetMetadataByOrgEmail)} results.Entities.Count: {results.Entities.Count}");

                    var entity = results.Entities.First();

                    return new CoreStudentMetadata
                    {
                        First = entity.GetAttributeValue<string>(CoreStudentFields.Given1),
                        Last = entity.GetAttributeValue<string>(CoreStudentFields.Surname),
                        Full = entity.GetAttributeValue<string>(CoreStudentFields.DisplayName),
                        Email = entity.GetAttributeValue<string>(CoreStudentFields.EmailAddress),
                        StudentId = entity.GetAttributeValue<int>(CoreStudentFields.StudentId),
                        CoreStudentId = entity.GetAttributeValue<Guid>(CoreStudentFields.CoreStudentId)
                    };
                }
                else
                {
                    throw new InvalidPluginExecutionException("No existing CoreStudents were found");
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"{nameof(GetMetadataByOrgEmail)} Exception: ", ex);
                throw;
            }
        }
    }
}