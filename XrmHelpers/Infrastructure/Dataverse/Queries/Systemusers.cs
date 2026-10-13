using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class Systemusers
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        public Systemusers(IDataverseContext context, IDataverseLoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.Create<Systemusers>();
        }
        public EntityReference GetReferenceByMetadata(string upn, string username)
        {
            _logger.WriteLog("Building RetrieveReference query expression on systemuser");

            if (string.IsNullOrWhiteSpace(upn) && string.IsNullOrWhiteSpace(username))
            {
                _logger.WriteLog("Systemusers.GetReferenceByMetadata called with null upn and username");
                throw new Exception();
            }

            var query = new QueryExpression("systemuser")
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    "internalemailaddress",
                    "systemuserid",
                    "domainname"
                )
            };

            var upnCondition = new ConditionExpression(
                "internalemailaddress",
                ConditionOperator.Equal,
                upn
            );

            var usernameCondition = new ConditionExpression(
                "domainname",
                ConditionOperator.Equal,
                username
            );

            if (string.IsNullOrWhiteSpace(upn) && username != null) query.Criteria.AddCondition(usernameCondition);
            if (upn != null && string.IsNullOrWhiteSpace(username)) query.Criteria.AddCondition(upnCondition);
            if (upn != null && username != null)
            {
                var filter = new FilterExpression(LogicalOperator.Or);
                filter.AddCondition(usernameCondition);
                filter.AddCondition(upnCondition);
                query.Criteria.AddFilter(filter);
            }

            _logger.WriteLog("Executing RetrieveReference query expression on systemuser");

            var results = _context.Service.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing RetrieveReference query expression");

            if (results.Entities.Count > 0)
            {
                var systemUserRef = new EntityReference(
                    "systemuser",
                    results.Entities[0].Id
                );

                _logger.WriteLog("SystemUserQueryService.RetrieveReference systemUserRef: {0}",
                    JsonSerializer.Serialize(
                        systemUserRef,
                        new JsonSerializerOptions
                        {
                            WriteIndented = true
                        }
                    )
                );

                return systemUserRef;
            }
            else
            {
                _logger.WriteLog("No existing entities were found, returning empty EntityReference");
                return new EntityReference();
            }
        }
        public EntityReference GetReferenceByAzureId(string azureId)
        {
            _logger.WriteLog("Building GetReferenceByAzureId query expression on systemuser");

            var query = new QueryExpression("systemuser")
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    "internalemailaddress",
                    "systemuserid",
                    "domainname",
                    "azureactivedirectoryobjectid"
                ),
                Criteria = new FilterExpression(LogicalOperator.Or)
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "azureactivedirectoryobjectid",
                            ConditionOperator.Equal,
                            azureId
                        )
                    }
                }
            };

            _logger.WriteLog("Executing GetReferenceByAzureId query expression on systemuser");

            var results = _context.Service.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing RetrieveReference query expression");

            if (results.Entities.Count > 0)
            {
                var systemUserRef = new EntityReference(
                    "systemuser",
                    results.Entities[0].Id
                );

                _logger.WriteLog("SystemUserQueryService.RetrieveReference systemUserRef: {0}",
                    JsonSerializer.Serialize(
                        systemUserRef,
                        new JsonSerializerOptions
                        {
                            WriteIndented = true
                        }
                    )
                );

                return systemUserRef;
            }
            else
            {
                _logger.WriteLog("No existing entities were found, returning empty EntityReference");
                return new EntityReference();
            }
        }
        public Entity GetMetadataById(Guid systemuserid)
        {
            _logger.WriteLog("Building RetrieveMetadata query expression on systemuser");

            var query = new QueryExpression("systemuser")
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    "azureactivedirectoryobjectid",
                    "internalemailaddress",
                    "systemuserid",
                    "isdisabled",
                    "domainname"
                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "systemuserid",
                            ConditionOperator.Equal,
                            systemuserid
                        )
                    }
                }
            };

            _logger.WriteLog("Executing RetrieveMetadata query expression on systemuser");

            var results = _context.Service.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing RetrieveMetadata query expression");

            if (results.Entities.Count > 0)
            {
                _logger.WriteLog("SystemUserQueryService.RetrieveMetadata results.Entities[0]: {0}",
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
                _logger.WriteLog("No existing entities were found");
                return new Entity();
            }
        }
        public UpsertResponse Upsert(string objectId, string upn)
        {
            var upsertUser = new Entity("systemuser");

            upsertUser["azureactivedirectoryobjectid"] = objectId;
            upsertUser["internalemailaddress"] = upn;

            UpsertRequest request = new UpsertRequest()
            {
                Target = upsertUser
            };

            return (UpsertResponse)_context.Service.Execute(request);
        }
    }
}