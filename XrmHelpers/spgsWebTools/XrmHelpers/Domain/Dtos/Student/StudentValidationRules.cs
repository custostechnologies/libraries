using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    // Rule 1: Ensure Given1 is not empty
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

    // Rule 2: Ensure StudentID is up to 8 digit numeric identifier
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

    // Rule 3: Ensure StudentID is > 0
    public class StudentIdMoreThanZeroRule : ValidationRule<StudentDto>
    {
        public override ValidationResult Validate(StudentDto entity)
        {
            if (entity.StudentId <= 0)
            {
                return Failure(
                    nameof(StudentDto.StudentId),
                    "StudentID must be greater than 0."
                );
            }

            return Success();
        }
    }

    // Rule 4: Ensure Surname is not empty
    public class SurnameNotEmptyRule : ValidationRule<StudentDto>
    {
        public override ValidationResult Validate(StudentDto entity)
        {
            return string.IsNullOrWhiteSpace(entity.Surname)
                ? Failure(
                    nameof(entity.Surname),
                    "StudentDto.Surname cannot be empty."
                )
                : Success();
        }
    }

    // Rule 5: Ensure House is not empty
    public class HouseNotEmptyRule : ValidationRule<StudentDto>
    {
        public override ValidationResult Validate(StudentDto entity)
        {
            return entity.House == null
                ? Failure(
                    nameof(entity.Surname),
                    "StudentDto.House cannot be null."
                )
                : Success();
        }
    }

    // Rule 6: Ensure Gender is not empty
    public class GenderNotEmptyRule : ValidationRule<StudentDto>
    {
        public override ValidationResult Validate(StudentDto entity)
        {
            return entity.Gender == null
                ? Failure(
                    nameof(entity.Gender),
                    "StudentDto.Gender cannot be null."
                )
                : Success();
        }
    }

    // Rule 7: Ensure Campus is not empty
    public class CampusNotEmptyRule : ValidationRule<StudentDto>
    {
        public override ValidationResult Validate(StudentDto entity)
        {
            return entity.Campus == null
                ? Failure(
                    nameof(entity.Campus),
                    "StudentDto.Campus cannot be null."
                )
                : Success();
        }
    }

    // Rule 8: Ensure Age is an integer greater than zero
    public class AgeMoreThanZeroRule : ValidationRule<StudentDto>
    {
        public override ValidationResult Validate(StudentDto entity)
        {
            if (!int.TryParse(entity.Age.ToString(), out int age) || age <= 0)
            {
                return Failure(
                    nameof(StudentDto.Age),
                    "Age must be numeric and greater than 0."
                );
            }

            return Success();
        }
    }

    // Rule 9: Ensure Year Level is an integer between -1 and 12 (inclusive)
    public class ValidYearLevelRule : ValidationRule<StudentDto>
    {
        public override ValidationResult Validate(StudentDto entity)
        {
            if (
                !int.TryParse(entity.YearLevel.ToString(), out int yearLevel)
                    || (yearLevel < -1 && yearLevel > 12))
            {
                return Failure(
                    nameof(StudentDto.YearLevel),
                    "YearLevel must be numeric and between -1 and 12 (inclusive)."
                );
            }

            return Success();
        }
    }
}