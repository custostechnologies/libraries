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
using System.IdentityModel.Tokens;

namespace custos.XrmHelpers.Domain.Dtos
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

        public HTMLSnippetDto? HTMLSnippet { get; set; }

        [DataverseLink(
            srcName: $"sn.{HTMLSnippetFields.InjectionTags}",
            srcType: typeof(AliasedValue),
            trgName: null,
            trgType: null)]
        public string InjectionTags { get; set; } = string.Empty;

        [DataverseLink(
            srcName: $"sn.{HTMLSnippetFields.Sequence}",
            srcType: typeof(AliasedValue),
            trgName: null,
            trgType: null)]
        public int Sequence { get; set; }

        [DataverseLink(
            srcName: $"sn.{HTMLSnippetFields.Description}",
            srcType: typeof(AliasedValue),
            trgName: null,
            trgType: null)]
        public string Description { get; set; } = string.Empty;
    }
}