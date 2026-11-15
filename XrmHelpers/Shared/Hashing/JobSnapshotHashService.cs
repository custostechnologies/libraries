using Microsoft.Xrm.Sdk;
using custos.XrmHelpers.Domain.Contracts;

namespace custos.XrmHelpers.Shared.Hashing
{
    public class JobSnapshotHashService
    {
        private readonly IDataverseLogger _logger;
        public JobSnapshotHashService(
            IDataverseLogger logger
        )
        {
            _logger = logger;
        }
        public (string plain, int encoded) Generate(JobPositionSnapshot snap)
        {
            string plaintextHash = string.Join("|",
                    new string[] {
                    snap.Name ?? "",
                    snap.StaffJobPositionsSeq.ToString(),
                    snap.SynReportsTo ?? "",
                    (snap.AssignmentFte ?? 0).ToString(),
                    (snap.FteRequired ?? 0).ToString(),
                    snap.StaffId.ToString(),
                    snap.IsActive.ToString(),
                    snap.StartDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                    snap.EndDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""
                    }.Select(val => val ?? string.Empty)
                );

            _logger.WriteLog("Plaintext hash: {0}", plaintextHash);

            var encodedHash = GetBKDRHashKey.Calculate(plaintextHash);

            return (plaintextHash, encodedHash);
        }
    }
}