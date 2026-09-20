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
    public class EmailTemplateDto : IDto
    {
        public string SourceEntityLogicalName => EntityLogicalNames.EmailTemplate;
        public string TargetEntityLogicalName => "";

        public DateTime? ForcePlugin { get; set; } = null;

        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection { };

        [DataverseLink(
            srcName: EmailTemplateFields.CC,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string? CC { get; set; }

        [DataverseLink(
            srcName: EmailTemplateFields.ContentBodyText,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string? ContentBodyText { get; set; }

        [DataverseLink(
            srcName: EmailTemplateFields.ContentCardHeader,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string? ContentCardHeader { get; set; }

        [DataverseLink(
            srcName: EmailTemplateFields.EmailTemplateId,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null
        )]
        public Guid? EmailTemplateId { get; set; }

        [DataverseLink(
            srcName: EmailTemplateFields.EmailTemplateKey,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string? EmailTemplateKey { get; set; }

        /*[DataverseLink(
            srcName: EmailTemplateFields.FetchXmlQuery,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null
        )]
        public Guid? FetchXmlQuery { get; set; }

        [DataverseLink(
            srcName: EmailTemplateFields.FetchXmlQuery,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public Guid? FetchXmlQuery_Name { get; set; }*/

        [DataverseLink(
            srcName: EmailTemplateFields.HeaderSubtitle,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string? HeaderSubtitle { get; set; }

        [DataverseLink(
            srcName: EmailTemplateFields.HeaderTitle,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string? HeaderTitle { get; set; }

        [DataverseLink(
            srcName: EmailTemplateFields.HtmlTemplate,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null
        )]
        public Guid? HtmlTemplate { get; set; }

        [DataverseLink(
            srcName: EmailTemplateFields.Name,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string? Name { get; set; }

        [DataverseLink(
            srcName: EmailTemplateFields.SendFrom,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string? SendFrom { get; set; }

        [DataverseLink(
            srcName: EmailTemplateFields.Subject,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string? Subject { get; set; }

        [DataverseLink(
            srcName: EmailTemplateFields.Theme,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        [DataverseReference(
            targetEntity: EntityLogicalNames.Theme
        )]
        public EntityReference? Theme { get; set; }
    }
}