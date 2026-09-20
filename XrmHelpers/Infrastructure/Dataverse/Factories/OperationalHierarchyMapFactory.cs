using System;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using System.Text.Json;
using System.Security.Claims;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class OperationalHierarchyMapFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private CoreStaffMembers? _coreStaffQuery;
        private Systemusers? _systemuserQuery;
        private CoreJobAssignments? _assignmentQuery;
        private OperationalHierarchyMaps? _operationalMapQuery;
        private CoreJobPositions? _coreJobPositionQuery;
        private string ENTITY_LOGICAL_NAME = EntityLogicalNames.OperationalHierarchyMap;
        public OperationalHierarchyMapFactory(
            IOrganizationService service,
            ITracingService
        tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public Guid Build(Entity assignment, Entity referenceMapEntity)
        {
            // position id is PARENT if isParent true
            _assignmentQuery = new CoreJobAssignments(_service, _tracer);
            _operationalMapQuery = new OperationalHierarchyMaps(_service, _tracer);
            _coreJobPositionQuery = new CoreJobPositions(_service, _tracer);

            EntityReference parentAssignmentRef;
            EntityReference childAssignmentRef;

            Guid parentPositionId = referenceMapEntity.GetAttributeValue<EntityReference>(ReferenceHierarchyMapFields.ParentJobPosition).Id;

            parentAssignmentRef = _assignmentQuery.GetMostRecentReferenceByPositionId(parentPositionId);

            if (parentAssignmentRef == null)
            {
                throw new InvalidPluginExecutionException("No assignment found for position {0}");
            }

            if (assignment.Id == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("assignment has Guid.Empty");
            }

            if (parentAssignmentRef == null)
            {
                throw new InvalidPluginExecutionException("parentAssignment is null");
            }

            if (parentAssignmentRef.Id == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("parentAssignment has Guid.Empty");
            }

            var existing = _operationalMapQuery.GetByCompositeKey(assignment.Id, parentAssignmentRef.Id);
            var parentPositionName = _coreJobPositionQuery.GetEntityNameById(parentAssignmentRef.Id) ?? "UNRESOLVED";
            var childPositionName = _coreJobPositionQuery.GetEntityNameById(assignment.Id) ?? "UNRESOLVED";
            string? campusName = referenceMapEntity.GetAttributeValue<OptionSetValue>(ReferenceHierarchyMapFields.Campus) != null
                ? ((Enums.Campus)referenceMapEntity.GetAttributeValue<OptionSetValue>(ReferenceHierarchyMapFields.Campus).Value).ToString()
                : null;

            if (existing == null || existing.Id == Guid.Empty)
            {
                Entity createMap = new Entity(ENTITY_LOGICAL_NAME);

                createMap[OperationalHierarchyMapFields.ChildJobAssignment] = assignment.ToEntityReference();
                createMap[OperationalHierarchyMapFields.ChildJobAssignmentIdx] = assignment.Id.ToString();
                createMap[OperationalHierarchyMapFields.CoreJobAssignment] = assignment.ToEntityReference();
                createMap[OperationalHierarchyMapFields.CoreJobAssignmentIdx] = assignment.Id.ToString();
                createMap[OperationalHierarchyMapFields.ParentJobAssignment] = parentAssignmentRef;
                createMap[OperationalHierarchyMapFields.ParentJobAssignmentIdx] = parentAssignmentRef.Id.ToString();

                createMap[OperationalHierarchyMapFields.ChildJobPosition] = referenceMapEntity.GetAttributeValue<EntityReference>(ReferenceHierarchyMapFields.ChildJobPosition);
                createMap[OperationalHierarchyMapFields.ChildJobPositionIdx] = referenceMapEntity.GetAttributeValue<EntityReference>(ReferenceHierarchyMapFields.ChildJobPosition).Id.ToString();
                createMap[OperationalHierarchyMapFields.CoreJobPosition] = referenceMapEntity.GetAttributeValue<EntityReference>(ReferenceHierarchyMapFields.CoreJobPosition);
                createMap[OperationalHierarchyMapFields.CoreJobPositionIdx] = referenceMapEntity.GetAttributeValue<EntityReference>(ReferenceHierarchyMapFields.CoreJobPosition).Id.ToString();
                createMap[OperationalHierarchyMapFields.ParentJobPosition] = referenceMapEntity.GetAttributeValue<EntityReference>(ReferenceHierarchyMapFields.ParentJobPosition);
                createMap[OperationalHierarchyMapFields.ParentJobPositionIdx] = referenceMapEntity.GetAttributeValue<EntityReference>(ReferenceHierarchyMapFields.ParentJobPosition).Id.ToString();

                createMap[OperationalHierarchyMapFields.IsActive] = true;
                createMap[OperationalHierarchyMapFields.LinkType] = referenceMapEntity.GetAttributeValue<OptionSetValue>(ReferenceHierarchyMapFields.LinkType);
                createMap[OperationalHierarchyMapFields.Name] = campusName == null
                    ? $"OPERATIONAL MAP: [Child: {childPositionName}] [Parent: {parentPositionName}]"
                    : $"OPERATIONAL MAP: [Child: {childPositionName}] [Campus: {campusName}] [Parent: {parentPositionName}]";
                createMap[OperationalHierarchyMapFields.SourceOfTruthId] = Guid.NewGuid().ToString();
                createMap[OperationalHierarchyMapFields.Status] = new OptionSetValue((int)Enums.Status.Active);
                createMap[OperationalHierarchyMapFields.StatusReason] = new OptionSetValue((int)Enums.StatusReason.Active);
                createMap[OperationalHierarchyMapFields.MapVersion] = DateTimeOffset.Now.ToString("yyyyMMddHHmm");

                if (referenceMapEntity.GetAttributeValue<int>(ReferenceHierarchyMapFields.ApprovalIndex) != null) createMap[OperationalHierarchyMapFields.ApprovalIndex] = referenceMapEntity.GetAttributeValue<int>(ReferenceHierarchyMapFields.ApprovalIndex);

                if (referenceMapEntity.GetAttributeValue<OptionSetValue>(ReferenceHierarchyMapFields.Campus) != null) createMap[OperationalHierarchyMapFields.Campus] = referenceMapEntity.GetAttributeValue<OptionSetValue>(ReferenceHierarchyMapFields.Campus);

                var createMapJson = JsonSerializer.Serialize(
                    createMap,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                );

                _tracer.Trace("createMap entity: {0}", createMapJson);

=======
>>>>>>> source-temp/refactor/decouple-xrmhelpers
                var mapId = _service.Create(createMap);

                _tracer.Trace("Created {0} with Id: {1}", ENTITY_LOGICAL_NAME, mapId);

                return mapId;
            }
            else
            {
                _tracer.Trace("Existing Operational Hierarchy Map with the same Child Assignment Id and Parent Assignment Id was detected. Exiting this method.");
                return Guid.Empty;
            }
        }
    }
}