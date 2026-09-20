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
using System.IdentityModel.Tokens;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class TemplateSnippetIntersectDto : IDto
    {
        public string SourceEntityLogicalName => EntityLogicalNames.TemplateSnippetIntersect;
        public string TargetEntityLogicalName => "";
        public DateTime? ForcePlugin { get; set; } = null;

        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection { };

        [DataverseLink(
            srcName: TemplateSnippetIntersectFields.InjectionOrder,
            srcType: typeof(int),
            trgName: null,
            trgType: null)]
        public int InjectionOrder { get; set; } = 0;

        [DataverseLink(
            srcName: TemplateSnippetIntersectFields.Name,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string Name { get; set; } = string.Empty;

        [DataverseLookup(
            keyProperty: null,
            targetEntity: EntityLogicalNames.EmailTemplate,
            targetAttribute: null)]
        [DataverseLink(
            srcName: TemplateSnippetIntersectFields.RelatedEmailTemplate,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public EntityReference? RelatedEmailTemplate { get; set; }

        [DataverseLink(
            srcName: TemplateSnippetIntersectFields.RelatedHTMLSnippet,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public EntityReference? RelatedHTMLSnippet { get; set; }

        [DataverseLink(
            srcName: TemplateSnippetIntersectFields.Status,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public string Status { get; set; } = string.Empty;

        [DataverseLink(
            srcName: TemplateSnippetIntersectFields.StatusReason,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public string StatusReason { get; set; } = string.Empty;
    }
}