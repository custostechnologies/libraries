using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    // Rule 1: Ensure string properties are populated
    public class Given1NotEmptyRule : IValidationRule<StudentDto>
    {
        public string ErrorMessage => "Student Given1 cannot be null or whitespace.";
        public bool IsValid(StudentDto student) =>
            !string.IsNullOrWhiteSpace(student.Given1);
    }

    // Rule 2: Ensure StudentID is up to 8 digits numeric identifier
    public class NumericStudentIdRule : IValidationRule<StudentDto>
    {
        public string ErrorMessage => "StudentID must a numeric identifier that is 8 digits or less and greater than zero.";
        public bool IsValid(StudentDto student)
        {
            var str = student.StudentId.ToString();
            return str.Length <= 8 && student.StudentId > 0;
        }
    }
}