using Microsoft.Xrm.Sdk;
using custos.XrmHelpers.Domain.Dtos;

namespace custos.XrmHelpers.Shared.Hashing
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentHashService
    {
        private readonly IDataverseLogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracer"></param>
        public StudentHashService(IDataverseLogger logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>

        public (string plain, int encoded) Generate(StudentDto student)
        {
            _logger.WriteLog("Entered StudentHashService.Generate");

            string plaintextHash = string.Join("|",
                new string[] {
                    student.StudentId.ToString() ?? "",
                    student.Age.ToString() ?? "",
                    student.IsActive.ToString() ?? "",
                    student.Campus.value.ToString() ?? "",
                    student.Gender.value.ToString() ?? "",
                    student.Given1 ?? "",
                    student.Given2 ?? "",
                    student.Preferred ?? "",
                    student.Surname ?? "",
                    student.House.value.ToString() ?? "",
                    student.YearLevel.ToString() ?? ""
                }.Select(val => val ?? string.Empty)
            );

            _logger.WriteLog("Plaintext hash: {0}", plaintextHash);

            var encodedHash = GetBKDRHashKey.Calculate(plaintextHash);
            _logger.WriteLog("EncodedHash within StudentHashService.Generate calculated as: {0}. Returning to call source.", encodedHash);

            return (plaintextHash, encodedHash);
        }
    }
}