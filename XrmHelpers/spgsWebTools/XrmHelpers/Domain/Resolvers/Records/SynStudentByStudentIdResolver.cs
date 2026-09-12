using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Diagnostics;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Resolvers
{
    public class SynStudentByStudentIdResolver : LookupResolver<int>
    {
        public SynStudentByStudentIdResolver(IDataverseContext dataverseContext) : base(dataverseContext)
        { }
        public override bool CanResolve(ILookupMetadata<int> metadata)
        {
            return metadata.TargetEntityLogicalName == EntityLogicalNames.SynStudent && metadata.TargetAttributeLogicalName == SynStudentFields.StudentId;
        }
    }
}