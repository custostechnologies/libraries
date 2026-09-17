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
        public string SourceEntityLogicalName => EntityLogicalNames.SynStaffMember;
        public string TargetEntityLogicalName => EntityLogicalNames.CoreStaffMember;

        [DataverseLink(srcName: SynStaffMemberFields.ForcePlugin, srcType: typeof(DateTime?), trgName: null, trgType: null)]

        public DateTime? ForcePlugin { get; set; } = null;
        
        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection{};
    }
}