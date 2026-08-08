using System;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class OperationalHierarchyMapFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private CoreStaffMembers? _coreStaffQuery;
        private Systemusers? _systemuserQuery;
        private CoreJobAssignments? _assignmentQuery;
        private string ENTITY_LOGICAL_NAME = EntityLogicalNames.OperationalHierarchyMap;
        public OperationalHierarchyMapFactory(
            IOrganizationService service,
            ITracingService
        tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public Guid BuildParent(Entity assignmentEntity, Entity referenceMapEntity)
        {
            _assignmentQuery = new CoreJobAssignments(_service, _tracer);

            Guid parentPosition = referenceMapEntity.GetAttributeValue<EntityReference>(ReferenceHierarchyMapFields.ParentJobPosition).Id;

            EntityReference parentAssignment = _assignmentQuery.GetMostRecentReferenceByPositionId(parentPosition);

            EntityReference childAssignment = new EntityReference(
                EntityLogicalNames.CoreJobAssignment,
                assignmentEntity.Id
            );

            /*
            var operationalMap = new OperationalHierarchyMapDto()
            {
                ChildJobAssignment = childAssignment,
                ChildJobAssignmentIdx = childAssignment.Id.ToString(),
                CoreJobAssignment = assignmentEntity.ToEntityReference(),
                CoreJobAssignmentIdx = assignmentEntity.Id.ToString(),
                ParentJobAssignment = parentAssignment,
                ParentJobAssignmentIdx = parentAssignment.Id.ToString(),
                IsActive = true,
                LinkType = referenceMapEntity.GetAttributeValue<OptionSetValue>(ReferenceHierarchyMapFields.LinkType),
                Name = $"OPERATIONAL MAP: {referenceMapEntity.GetAttributeValue<string>(ReferenceHierarchyMapFields.Name)}",
                SourceOfTruthId = Guid.NewGuid().ToString(),
                Status = new OptionSetValue((int)Enums.Status.Active),
                StatusReason = new OptionSetValue((int)Enums.StatusReason.Active)
            };
            */
            Entity createMap = new Entity(
                ENTITY_LOGICAL_NAME
            );

            createMap[OperationalHierarchyMapFields.ChildJobAssignment] = childAssignment;
            createMap[OperationalHierarchyMapFields.ChildJobAssignmentIdx] = childAssignment.Id.ToString();
            createMap[OperationalHierarchyMapFields.CoreJobAssignment] = assignmentEntity.ToEntityReference();
            createMap[OperationalHierarchyMapFields.CoreJobAssignmentIdx] = assignmentEntity.Id.ToString();
            createMap[OperationalHierarchyMapFields.ParentJobAssignment] = parentAssignment;
            createMap[OperationalHierarchyMapFields.ParentJobAssignmentIdx] = parentAssignment.Id.ToString();
            createMap[OperationalHierarchyMapFields.IsActive] = true;
            createMap[OperationalHierarchyMapFields.LinkType] = referenceMapEntity.GetAttributeValue<OptionSetValue>(ReferenceHierarchyMapFields.LinkType);
            createMap[OperationalHierarchyMapFields.Name] = $"OPERATIONAL MAP: {referenceMapEntity.GetAttributeValue<string>(ReferenceHierarchyMapFields.Name)}";
            createMap[OperationalHierarchyMapFields.SourceOfTruthId] = Guid.NewGuid().ToString();
            createMap[OperationalHierarchyMapFields.Status] = new OptionSetValue((int)Enums.Status.Active);
            createMap[OperationalHierarchyMapFields.StatusReason] = new OptionSetValue((int)Enums.StatusReason.Active);

            return _service.Create(createMap);
        }
    }
}