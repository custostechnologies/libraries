using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    // Rule 1: Ensure Given1 is not empty
    public class Given1NotEmptyStaffRule : ValidationRule<StaffMemberDto>
    {
        public override ValidationResult Validate(StaffMemberDto dto)
        {
            return string.IsNullOrWhiteSpace(dto.GivenName1)
                ? Failure(
                    nameof(dto.GivenName1),
                    "StaffMemberDto.GivenName1 cannot be empty."
                )
                : Success();
        }
    }

    // Rule 2: Ensure StaffIdID is up to 8 digit numeric identifier
    public class NumericStaffIdStaffRule : ValidationRule<StaffMemberDto>
    {
        public override ValidationResult Validate(StaffMemberDto dto)
        {
            if (!int.TryParse(dto.StaffId.ToString(), out int id) || id.ToString().Length > 8)
            {
                return Failure(
                    nameof(StaffMemberDto.StaffId),
                    "StaffId must be numeric and less than 8 digits."
                );
            }

            return Success();
        }
    }

    // Rule 3: Ensure StudentID is > 0
    public class StaffIdMoreThanZeroStaffRule : ValidationRule<StaffMemberDto>
    {
        public override ValidationResult Validate(StaffMemberDto dto)
        {
            if (dto.StaffId <= 0)
            {
                return Failure(
                    nameof(StaffMemberDto.StaffId),
                    "StaffId must be greater than 0."
                );
            }

            return Success();
        }
    }

    // Rule 4: Ensure Surname is not empty
    public class SurnameNotEmptyStaffRule : ValidationRule<StaffMemberDto>
    {
        public override ValidationResult Validate(StaffMemberDto dto)
        {
            return string.IsNullOrWhiteSpace(dto.LastName)
                ? Failure(
                    nameof(dto.LastName),
                    "StaffMemberDto.LastName cannot be empty."
                )
                : Success();
        }
    }

    // Rule 5: Ensure House is not empty
    public class CoreStaffCategoryNotEmptyStaffRule : ValidationRule<StaffMemberDto>
    {
        public override ValidationResult Validate(StaffMemberDto dto)
        {
            return dto.CoreStaffCategory == null
                ? Failure(
                    nameof(dto.CoreStaffCategory),
                    "StaffMemberDto.CoreStaffCategory cannot be null."
                )
                : Success();
        }
    }

    // Rule 6: Ensure Gender is not empty
    public class GenderNotEmptyStaffRule : ValidationRule<StaffMemberDto>
    {
        public override ValidationResult Validate(StaffMemberDto dto)
        {
            /*return dto.Gender == null
                ? Failure(
                    nameof(dto.Gender),
                    "StaffMemberDto.Gender cannot be null."
                )
                : Success();*/
            return Success();
        }
    }

    // Rule 7: Ensure Campus is not empty
    public class CampusNotEmptyStaffRule : ValidationRule<StaffMemberDto>
    {
        public override ValidationResult Validate(StaffMemberDto dto)
        {
            /*return dto.Campus == null
                ? Failure(
                    nameof(dto.Campus),
                    "StaffMemberDto.Campus cannot be null."
                )
                : Success();*/
            return Success();
        }
    }
}