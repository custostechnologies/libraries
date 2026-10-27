using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.XrmHelpers.Shared.Hashing
{
    /// <summary>
    /// 
    /// </summary>
    public class StaffMemberHashService
    {
        private readonly IDataverseLogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracer"></param>
        public StaffMemberHashService(IDataverseLogger logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>

        public (string plain, int encoded) Generate(StaffMemberSnapshot staff)
        {
            _logger.WriteLog("Entered StaffMemberHashService.Generate");

            string plaintextHash = string.Join("|",
                new string[] {
                    staff.StaffCampus ?? "",
                    staff.StaffCategory ?? "",
                    staff.SchoolStaffCode ?? "",
                    staff.StaffDepartment ?? "",
                    staff.Gender ?? "",
                    staff.Given1 ?? "",
                    staff.Given2 ?? "",
                    staff.NameInternal ?? "",
                    staff.Surname ?? "",
                    staff.NetworkLogin ?? "",
                    staff.MaidenName ?? "",
                    staff.StaffPreferredName ?? "",
                    staff.Email ?? "",
                    staff.SurnamePrevious ?? "",
                    staff.SubSchool ?? "",
                    staff.SynMirrorId.ToString(),
                    staff.OccupEmail ?? "",
                    staff.OccupPhone ?? "",
                    staff.StaffId.ToString(),
                    staff.ActiveFlag.ToString(),
                    staff.LeavingDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                    staff.StartDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""
                }.Select(val => val ?? string.Empty)
            );

            _logger.WriteLog("Plaintext hash: {0}", plaintextHash);

            var encodedHash = GetBKDRHashKey.Calculate(plaintextHash);
            _logger.WriteLog("EncodedHash within StaffBDKRHashService.Generate calculated as: {0}. Returning to call source.", encodedHash);

            return (plaintextHash, encodedHash);
        }
    }
}