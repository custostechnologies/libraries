using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;

namespace spgsWebTools.XrmHelpers.Shared.Hashing
{
    public class JobSnapshotHashService
    {
        private readonly ITracingService _tracer;
        public JobSnapshotHashService(
            ITracingService tracer
        )
        {
            _tracer = tracer;
        }
        public int Generate(JobPositionSnapshot snap)
        {
            string plaintextHash = string.Join("|",
                    new string[] {
                    snap.Name ?? "",
                    snap.StaffJobPositionsSeq.ToString(),
                    snap.SynReportsTo ?? "",
                    snap.AssignmentFte.ToString(),
                    snap.FteRequired.ToString(),
                    snap.StaffId.ToString(),
                    snap.IsActive.ToString(),
                    snap.StartDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                    snap.EndDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""
                    }.Select(val => val ?? string.Empty)
                );

            _tracer.Trace("Plaintext hash: {0}", plaintextHash);

            snap.PlaintextHash = plaintextHash;

            return GetBKDRHashKey.Calculate(plaintextHash);
        }
    }
}