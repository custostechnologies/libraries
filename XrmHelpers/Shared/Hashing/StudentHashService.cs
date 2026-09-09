using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;

namespace spgsWebTools.XrmHelpers.Shared.Hashing
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentHashService
    {
        private readonly ITracingService _tracer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracer"></param>
        public StudentHashService(ITracingService tracer)
        {
            _tracer = tracer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>

        public (string plain, int encoded) Generate(StudentSnapshot student)
        {
            _tracer.Trace("Entered StudentHashService.Generate");

            string plaintextHash = string.Join("|",
                new string[] {
                    student.StudentId.ToString() ?? "",
                    student.Age.ToString() ?? "",
                    student.StudentActiveFlag.ToString() ?? "",
                    student.StudentCampus ?? "",
                    student.StudentGender ?? "",
                    student.StudentGiven1 ?? "",
                    student.StudentGiven2 ?? "",
                    student.StudentPreferred ?? "",
                    student.StudentSurname ?? "",
                    student.StudentHouse ?? "",
                    student.StudentYearLevel.ToString() ?? "",
                    student.StudentPreferred ?? ""
                }.Select(val => val ?? string.Empty)
            );

            _tracer.Trace("Plaintext hash: {0}", plaintextHash);

            var encodedHash = GetBKDRHashKey.Calculate(plaintextHash);
            _tracer.Trace("EncodedHash within StudentHashService.Generate calculated as: {0}. Returning to call source.", encodedHash);

            return (plaintextHash, encodedHash);
        }
    }
}