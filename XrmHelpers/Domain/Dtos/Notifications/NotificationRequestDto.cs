using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using custos.XrmHelpers.Domain.Attributes;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Domain.Contracts;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace custos.XrmHelpers.Domain.Dtos
{
    /// <summary>
    ///
    /// </summary>
    public class NotificationRequestDto : IDto
    {
        public string SourceEntityLogicalName => "";

        public string TargetEntityLogicalName => EntityLogicalNames.NotificationRequest;

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: null,
            trgType: null
        )]
        public DateTime? ForcePlugin { get; set; } = null;

        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection { };

        [DataverseLookup(
            keyProperty: nameof(AdaptiveCardTemplateFields.TemplateKey),
            targetEntity: EntityLogicalNames.AdaptiveCardTemplate,
            targetAttribute: AdaptiveCardTemplateFields.TemplateKey
        )]
        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.AdaptiveCardTemplate,
            trgType: typeof(EntityReference)
        )]
        public EntityReference? AdaptiveCardTemplate { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.CCString,
            trgType: typeof(string)
        )]
        public string? CCString { get; set; }

        [DataverseLookup(
            keyProperty: nameof(EmailTemplateFields.EmailTemplateKey),
            targetEntity: EntityLogicalNames.EmailTemplate,
            targetAttribute: EmailTemplateFields.EmailTemplateKey
        )]
        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.EmailTemplate,
            trgType: typeof(EntityReference)
        )]
        public EntityReference? EmailTemplate { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.InFlowInputs,
            trgType: typeof(string)
        )]
        public string? InFlowInputs { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.InFlowInputsObject,
            trgType: typeof(string)
        )]
        public string? InFlowInputsObject { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.InputJson,
            trgType: typeof(File)
        )]
        public Guid? InputJSON { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.InputJson,
            trgType: typeof(File)
        )]
        public string? InputJSON_Name { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.MessageId,
            trgType: typeof(string)
        )]
        public string? MessageId { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.NotificationRequestId,
            trgType: typeof(Guid)
        )]
        public Guid? NotificationRequestId { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.NotificationType,
            trgType: typeof(OptionSetValue)
        )]
        public NotificationType NotificationType { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.RecipientString,
            trgType: typeof(string)
        )]
        public string? RecipientString { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.RelatedRecordId,
            trgType: typeof(string)
        )]
        public Guid? RegardingRecordId { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.RequestTitle,
            trgType: typeof(string)
        )]
        public string? RequestTitle { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.Importance,
            trgType: typeof(int)
        )]
        public int Importance { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.Subject,
            trgType: typeof(string)
        )]
        public string? Subject { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: NotificationRequestFields.SendFrom,
            trgType: typeof(string)
        )]
        public string? SendFrom { get; set; }
    }
}