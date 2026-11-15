using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Domain.Contracts;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace Custos.XrmHelpers.Domain.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class HTMLSnippetDto : IDto
    {
        public string SourceEntityLogicalName => EntityLogicalNames.HTMLSnippet;
        public string TargetEntityLogicalName => "";
        public DateTime? ForcePlugin { get; set; } = null;

        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection { };

        [DataverseLink(
            srcName: HTMLSnippetFields.Description,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string? Description { get; set; }

        [DataverseLink(
            srcName: HTMLSnippetFields.HTMLFile,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null
        )]
        public Guid HTMLFile { get; set; }

        [DataverseLink(
            srcName: HTMLSnippetFields.HTMLFile,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string HTMLFile_Name { get; set; } = string.Empty;

        [DataverseLink(
            srcName: HTMLSnippetFields.InjectionTags,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string InjectionTags { get; set; } = string.Empty;

        [DataverseLink(
            srcName: HTMLSnippetFields.Name,
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string Name { get; set; } = string.Empty;

        [DataverseLink(
            srcName: HTMLSnippetFields.Sequence,
            srcType: typeof(int),
            trgName: null,
            trgType: null
        )]
        public int Sequence { get; set; }

        [DataverseLink(
            srcName: HTMLSnippetFields.HTMLSnippetId,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null
        )]
        public Guid HTMLSnippetId { get; set; }
    }
}