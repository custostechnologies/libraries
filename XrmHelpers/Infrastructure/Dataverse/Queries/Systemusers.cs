using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class Systemusers
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        public Systemusers(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public EntityReference GetReferenceByMetadata(string upn, string username)
        {
            _tracer.Trace("Building RetrieveReference query expression on systemuser");

            var query = new QueryExpression("systemuser")
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    "internalemailaddress",
                    "systemuserid",
                    "domainname"
                ),
                Criteria = new FilterExpression(LogicalOperator.Or)
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "internalemailaddress",
                            ConditionOperator.Equal,
                            upn
                        ),
                        new ConditionExpression(
                            "domainname",
                            ConditionOperator.Equal,
                            username
                        )
                    }
                }
            };

            _tracer.Trace("Executing RetrieveReference query expression on systemuser");

            var results = _service.RetrieveMultiple(query);

            _tracer.Trace("Finished executing RetrieveReference query expression");

            if (results.Entities.Count > 0)
            {
                var systemUserRef = new EntityReference(
                    "systemuser",
                    results.Entities[0].Id
                );

                _tracer.Trace("SystemUserQueryService.RetrieveReference systemUserRef: {0}",
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
                _tracer.Trace("No existing entities were found, returning empty EntityReference");
                return new EntityReference(
                    "systemuser"
                );
            }
        }
        public EntityReference GetReferenceByAzureId(string azureId)
        {
            _tracer.Trace("Building GetReferenceByAzureId query expression on systemuser");

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

            _tracer.Trace("Executing GetReferenceByAzureId query expression on systemuser");

            var results = _service.RetrieveMultiple(query);

            _tracer.Trace("Finished executing RetrieveReference query expression");

            if (results.Entities.Count > 0)
            {
                var systemUserRef = new EntityReference(
                    "systemuser",
                    results.Entities[0].Id
                );

                _tracer.Trace("SystemUserQueryService.RetrieveReference systemUserRef: {0}",
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
                _tracer.Trace("No existing entities were found, returning empty EntityReference");
                return new EntityReference(
                    "systemuser"
                );
            }
        }
        public Entity GetMetadataById(Guid systemuserid)
        {
            _tracer.Trace("Building RetrieveMetadata query expression on systemuser");

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

            _tracer.Trace("Executing RetrieveMetadata query expression on systemuser");

            var results = _service.RetrieveMultiple(query);

            _tracer.Trace("Finished executing RetrieveMetadata query expression");

            if (results.Entities.Count > 0)
            {
                _tracer.Trace("SystemUserQueryService.RetrieveMetadata results.Entities[0]: {0}",
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
                return new Entity("systemuser");
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

            return (UpsertResponse)_service.Execute(request);
        }
    }
}