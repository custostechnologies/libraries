using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Schemas;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class CoreStaffMembers
    {
        public class CoreStaffMemberMetadata
        {
            public string First { get; set; } = string.Empty;
            public string Last { get; set; } = string.Empty;
            public string Full { get; set; } = string.Empty;
            public int StaffId { get; set; }
            public string Email { get; set; } = string.Empty;
            public Guid CoreStaffMemberId { get; set; }
            public EntityReference? User { get; set; }
        }
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

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                _logger.WriteLog($"Finished executing ExecuteAlternate CoreStaffMembers.GetEntityByTruth query expression, results.Entities.Count: {results.Entities.Count}");

                if (results.Entities.Count > 0)
                {
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

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing ExecuteAlternate query expression");

                if (results.Entities.Count > 0)
                {
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

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

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
        public CoreStaffMemberMetadata GetMetadataByStaffId(int staffId)
        {
            try
            {
                _logger.WriteLog($"Building GetEntityReferenceByStaffId query expression on {ENTITY_LOGICAL_NAME}");

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet(
                        CoreStaffMemberFields.StaffId,
                        CoreStaffMemberFields.DisplayName,
                        CoreStaffMemberFields.GivenName1,
                        CoreStaffMemberFields.LastName,
                        CoreStaffMemberFields.WorkEmail,
                        CoreStaffMemberFields.CoreStaffMemberId,
                        CoreStaffMemberFields.User
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

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing ExecuteAlternate CoreStaffMembers.GetReferenceByStaffId query expression");

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"ExecuteAlternate CoreStaffMembers.GetReferenceByStaffId results.Entities.Count: {results.Entities.Count}");

                    var entity = results.Entities.First();

                    return new CoreStaffMemberMetadata
                    {
                        First = entity.GetAttributeValue<string>(CoreStaffMemberFields.GivenName1),
                        Last = entity.GetAttributeValue<string>(CoreStaffMemberFields.LastName),
                        Full = entity.GetAttributeValue<string>(CoreStaffMemberFields.DisplayName),
                        Email = entity.GetAttributeValue<string>(CoreStaffMemberFields.WorkEmail),
                        StaffId = entity.GetAttributeValue<int>(CoreStaffMemberFields.StaffId),
                        CoreStaffMemberId = entity.GetAttributeValue<Guid>(CoreStaffMemberFields.CoreStaffMemberId),
                        User = entity.GetAttributeValue<EntityReference>(CoreStaffMemberFields.User)
                    };
                }
                else
                {
                    throw new InvalidPluginExecutionException("No existing CoreStaffMembers were found");
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("GetMetadataByStaffId Exception:");
                _logger.WriteLog(ex.ToString());
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception:");
                    _logger.WriteLog(ex.InnerException.ToString());
                }
                throw;
            }
        }
        public CoreStaffMemberMetadata GetMetadataByOrgEmail(string email)
        {
            try
            {
                _logger.WriteLog($"Building GetEntityReferenceByStaffId query expression on {ENTITY_LOGICAL_NAME}");

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet(
                        CoreStaffMemberFields.StaffId,
                        CoreStaffMemberFields.DisplayName,
                        CoreStaffMemberFields.GivenName1,
                        CoreStaffMemberFields.LastName,
                        CoreStaffMemberFields.WorkEmail,
                        CoreStaffMemberFields.CoreStaffMemberId,
                        CoreStaffMemberFields.User
                    ),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                CoreStaffMemberFields.WorkEmail,
                                ConditionOperator.Equal,
                                email
                            )
                        }
                    }
                };

                _logger.WriteLog($"Executing GetReferenceByStaffId query expression on {ENTITY_LOGICAL_NAME}");

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing ExecuteAlternate CoreStaffMembers.GetReferenceByStaffId query expression");

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"ExecuteAlternate CoreStaffMembers.GetReferenceByStaffId results.Entities.Count: {results.Entities.Count}");

                    var entity = results.Entities.First();

                    return new CoreStaffMemberMetadata
                    {
                        First = entity.GetAttributeValue<string>(CoreStaffMemberFields.GivenName1),
                        Last = entity.GetAttributeValue<string>(CoreStaffMemberFields.LastName),
                        Full = entity.GetAttributeValue<string>(CoreStaffMemberFields.DisplayName),
                        Email = entity.GetAttributeValue<string>(CoreStaffMemberFields.WorkEmail),
                        StaffId = entity.GetAttributeValue<int>(CoreStaffMemberFields.StaffId),
                        CoreStaffMemberId = entity.GetAttributeValue<Guid>(CoreStaffMemberFields.CoreStaffMemberId),
                        User = entity.GetAttributeValue<EntityReference>(CoreStaffMemberFields.User)
                    };
                }
                else
                {
                    throw new InvalidPluginExecutionException("No existing CoreStaffMembers were found");
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("GetMetadataByStaffId Exception:");
                _logger.WriteLog(ex.ToString());
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception:");
                    _logger.WriteLog(ex.InnerException.ToString());
                }
                throw;
            }
        }
        public Entity GetEntityByOrgEmail(string email)
        {
            try
            {
                _logger.WriteLog($"Building GetEntityByOrgEmail query expression on {ENTITY_LOGICAL_NAME}");

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = CoreStaffMemberSchema.SyncColumns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                CoreStaffMemberFields.WorkEmail,
                                ConditionOperator.Equal,
                                email
                            )
                        }
                    }
                };

                _logger.WriteLog($"Executing GetEntityByOrgEmail query expression on {ENTITY_LOGICAL_NAME}");

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing RetrieveMultiple query expression");

                if (results.Entities.Count > 0)
                {
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
    }
}