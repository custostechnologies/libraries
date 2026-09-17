using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationRequestDto : IDto
    {
        public string SourceEntityLogicalName => EntityLogicalNames.SynStaffMember;
        public string TargetEntityLogicalName => EntityLogicalNames.CoreStaffMember;

        [DataverseLink(srcName: SynStaffMemberFields.ForcePlugin, srcType: typeof(DateTime?), trgName: null, trgType: null)]
        public DateTime? ForcePlugin { get; set; } = null;

        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection{};

        public EntityReference? AdaptiveCardTemplate { get; set; }

        public string? CCString { get; set; }
        public EntityReference? EmailTemplate { get; set; }
        public EntityReference? ExportTemplate { get; set; }
        public string? InFlowInputs { get; set; }

        public string? InFlowInputsObject { get; set; }

        public Guid? InputJSON { get; set; }

        public string? InputJSON_Name { get; set; }

        public string? MessageId { get; set; }

        public Guid? NotificationRequestId { get; set; }
        
        public NotificationType NotificationType { get; set; }

        public string? RecipientString { get; set; }

        public Guid? RegardingRecordId { get; set; }

        public string? RequestTitle { get; set; }
    }
}