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
        public string TargetEntityLogicalName => EntityLogicalNames.TemplateSnippetIntersect;

        [DataverseLink(srcName: SynStaffMemberFields.ForcePlugin, srcType: typeof(DateTime?), trgName: null, trgType: null)]
        public DateTime? ForcePlugin { get; set; } = null;
        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection{};

        public int InjectionOrder { get; set; } = 0;

        public string Name  { get; set; } = string.Empty;
        
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

        [DataverseLink(srcName: TemplateSnippetIntersectFields.RelatedHTMLSnippet, srcType: typeof(EntityReference), trgName: null, trgType: null)]
        public EntityReference? RelatedHTMLSnippet  { get; set; }
    }
}