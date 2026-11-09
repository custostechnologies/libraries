using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    /// <summary>
    ///
    /// </summary>
    public class ComplianceItemDto : NotifiableDto<ComplianceItemDto>, IActivityDto, IValidationMetadata, IQueryDto
    {
        public ComplianceItemDto() : base() { }

        public override string TargetAppDisplayName => "manageCompliance";
        public string RegardingRecordAppDisplayName => "manageCompliance";
        public string RegardingRecordEntityLogicalName => EntityLogicalNames.Comp;
        public override string SourceEntityLogicalName => EntityLogicalNames.WorkersCompensationClaim;
        public override string TargetEntityLogicalName => EntityLogicalNames.WorkersCompensationClaim;
        
    }
}