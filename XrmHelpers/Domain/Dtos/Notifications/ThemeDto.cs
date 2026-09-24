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
    public class ThemeDto : IDto
    {
        public string SourceEntityLogicalName => EntityLogicalNames.Theme;
        public string TargetEntityLogicalName => "";

        public DateTime? ForcePlugin { get; set; } = null;

        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection { };

        [DataverseLink(
            srcName: ThemeFields.Name,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string Name { get; set; } = string.Empty;

        [DataverseLink(
            srcName: ThemeFields.Primary,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string Primary { get; set; } = string.Empty;

        [DataverseLink(
            srcName: ThemeFields.Secondary,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string Secondary { get; set; } = string.Empty;

        [DataverseLink(
            srcName: ThemeFields.Highlight,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string Highlight { get; set; } = string.Empty;

        [DataverseLink(
            srcName: ThemeFields.Alternate,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string Alternate { get; set; } = string.Empty;
    }
}