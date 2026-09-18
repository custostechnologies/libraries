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
        public string SourceEntityLogicalName => EntityLogicalNames.NotificationRequest;
        public string TargetEntityLogicalName => EntityLogicalNames.NotificationRequest;

        [DataverseLink(srcName: SynStaffMemberFields.ForcePlugin, srcType: typeof(DateTime?), trgName: null, trgType: null)]
        public DateTime? ForcePlugin { get; set; } = null;
        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection { };

        [DataverseLookup(keyProperty: nameof(AdaptiveCardTemplateFields.TemplateKey), targetEntity: EntityLogicalNames.AdaptiveCardTemplate, targetAttribute: AdaptiveCardTemplateFields.TemplateKey)]
        [DataverseLink(srcName: NotificationRequestFields.AdaptiveCardTemplate, srcType: typeof(EntityReference), trgName: NotificationRequestFields.AdaptiveCardTemplate, trgType: typeof(EntityReference))]
        public EntityReference? AdaptiveCardTemplate { get; set; }

        [DataverseLink(srcName: NotificationRequestFields.CCString, srcType: typeof(string), trgName: NotificationRequestFields.CCString, trgType: typeof(string))]
        public string? CCString { get; set; }

        [DataverseLookup(keyProperty: nameof(EmailTemplateFields.EmailTemplateKey), targetEntity: EntityLogicalNames.EmailTemplate, targetAttribute: EmailTemplateFields.EmailTemplateKey)]
        [DataverseLink(srcName: NotificationRequestFields.EmailTemplate, srcType: typeof(EntityReference), trgName: NotificationRequestFields.EmailTemplate, trgType: typeof(EntityReference))]
        public EntityReference? EmailTemplate { get; set; }

        [DataverseLink(srcName: NotificationRequestFields.InFlowInputs, srcType: typeof(string), trgName: NotificationRequestFields.InFlowInputs, trgType: typeof(string))]
        public string? InFlowInputs { get; set; }

        [DataverseLink(srcName: NotificationRequestFields.InFlowInputsObject, srcType: typeof(string), trgName: NotificationRequestFields.InFlowInputsObject, trgType: typeof(string))]
        public string? InFlowInputsObject { get; set; }

        [DataverseLink(srcName: NotificationRequestFields.InputJson, srcType: typeof(Guid), trgName: NotificationRequestFields.InputJson, trgType: typeof(File))]
        public Guid? InputJSON { get; set; }

        [DataverseLink(srcName: NotificationRequestFields.InputJson, srcType: typeof(string), trgName: NotificationRequestFields.InputJson, trgType: typeof(File))]
        public string? InputJSON_Name { get; set; }

        [DataverseLink(srcName: NotificationRequestFields.MessageId, srcType: typeof(string), trgName: NotificationRequestFields.MessageId, trgType: typeof(string))]
        public string? MessageId { get; set; }

        [DataverseLink(srcName: NotificationRequestFields.NotificationRequestId, srcType: typeof(Guid), trgName: NotificationRequestFields.NotificationRequestId, trgType: typeof(Guid))]
        public Guid? NotificationRequestId { get; set; }

        [DataverseLink(srcName: NotificationRequestFields.NotificationType, srcType: typeof(NotificationType), trgName: NotificationRequestFields.NotificationType, trgType: typeof(OptionSetValue))]
        public NotificationType NotificationType { get; set; }

        [DataverseLink(srcName: NotificationRequestFields.RecipientString, srcType: typeof(string), trgName: NotificationRequestFields.RecipientString, trgType: typeof(string))]
        public string? RecipientString { get; set; }

        [DataverseLink(srcName: NotificationRequestFields.RelatedRecordId, srcType: typeof(string), trgName: NotificationRequestFields.RelatedRecordId, trgType: typeof(string))]
        public Guid? RegardingRecordId { get; set; }

        [DataverseLink(srcName: NotificationRequestFields.RequestTitle, srcType: typeof(string), trgName: NotificationRequestFields.RequestTitle, trgType: typeof(string))]
        public string? RequestTitle { get; set; }
    }
}