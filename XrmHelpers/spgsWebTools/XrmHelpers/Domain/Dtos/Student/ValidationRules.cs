using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    // Rule 1: Ensure string properties are populated
    public class Given1NotEmptyRule : ValidationRule<StudentDto>
    {
        public override ValidationResult Validate(StudentDto entity)
        {
            return string.IsNullOrWhiteSpace(entity.Given1)
                ? Failure(
                    nameof(entity.Given1),
                    "StudentDto.Given1 cannot be empty."
                )
                : Success();
        }
    }

    // Rule 2: Ensure StudentID is up to 8 digits numeric identifier
    public class NumericStudentIdRule : ValidationRule<StudentDto>
    {
        public override ValidationResult Validate(StudentDto entity)
        {
            if (!int.TryParse(entity.StudentId.ToString(), out int id) || id.ToString().Length > 8)
            {
                return Failure(
                    nameof(StudentDto.StudentId),
                    "StudentID must be numeric and less than 8 digits."
                );
            }

            return Success();
        }
    }
}