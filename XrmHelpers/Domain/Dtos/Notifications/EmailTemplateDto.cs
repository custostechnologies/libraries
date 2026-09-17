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
        public string TargetEntityLogicalName => EntityLogicalNames.EmailTemplate;

        [DataverseLink(srcName: SynStaffMemberFields.ForcePlugin, srcType: typeof(DateTime?), trgName: null, trgType: null)]

        public DateTime? ForcePlugin { get; set; } = null;
        
        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection{};

        [DataverseLink(srcName: EmailTemplateFields.CC, srcType: typeof(string), trgName: EmailTemplateFields.CC, trgType: typeof(string))]
        public string? CC { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.ContentBodyText, srcType: typeof(string), trgName: EmailTemplateFields.ContentBodyText, trgType: typeof(string))]
        public string? ContentBodyText { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.ContentCardHeader, srcType: typeof(string), trgName: EmailTemplateFields.ContentCardHeader, trgType: typeof(string))]
        public string? ContentCardHeader { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.EmailTemplateId, srcType: typeof(Guid), trgName: EmailTemplateFields.EmailTemplateId, trgType: typeof(Guid))]
        public Guid? EmailTemplateId { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.EmailTemplateKey, srcType: typeof(string), trgName: EmailTemplateFields.EmailTemplateKey, trgType: typeof(string))]
        public string? EmailTemplateKey { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.FetchXmlQuery, srcType: typeof(Guid), trgName: EmailTemplateFields.FetchXmlQuery, trgType: typeof(File))]
        public Guid? FetchXmlQuery { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.FetchXmlQuery, srcType: typeof(string), trgName: EmailTemplateFields.FetchXmlQuery, trgType: typeof(File))]
        public Guid? FetchXmlQuery_Name { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.HeaderSubtitle, srcType: typeof(string), trgName: EmailTemplateFields.HeaderSubtitle, trgType: typeof(string))]
        public string? HeaderSubtitle { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.HeaderTitle, srcType: typeof(string), trgName: EmailTemplateFields.HeaderTitle, trgType: typeof(string))]
        public string? HeaderTitle { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.HtmlTemplate, srcType: typeof(Guid), trgName: EmailTemplateFields.HtmlTemplate, trgType: typeof(File))]
        public Guid? HtmlTemplate { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.HtmlTemplate, srcType: typeof(string), trgName: EmailTemplateFields.HtmlTemplate, trgType: typeof(File))]
        public Guid? HtmtlTemplate_Name { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.Name, srcType: typeof(string), trgName: EmailTemplateFields.Name, trgType: typeof(string))]
        public string? Name { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.SendFrom, srcType: typeof(string), trgName: EmailTemplateFields.SendFrom, trgType: typeof(string))]
        public string? SendFrom { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.Subject, srcType: typeof(string), trgName: EmailTemplateFields.Subject, trgType: typeof(string))]
        public string? Subject { get; set; }

        [DataverseLink(srcName: EmailTemplateFields.Theme, srcType: typeof(EntityReference), trgName: EmailTemplateFields.Theme, trgType: typeof(EntityReference))]
        public EntityReference? Theme { get; set; }
    }
}