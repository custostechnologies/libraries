using System;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Models
{
    public interface ICoreUser
    {
        public Guid? CoreUserId { get; set; }
    }
    public class SynergeticStudent
    {
        [LogicalName(SynStaffMemberFields.SynMirrorId)]
        public Guid? SynMirrorId { get; set; }
    }
}